// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

namespace Fahrenheit;

/* [fkelava 24/10/26 12:41]
 * A simplified port of DirectXTK's WICTextureLoader.
 * https://github.com/microsoft/DirectXTK/blob/main/Src/WICTextureLoader.cpp
 *
 * Comments are preserved where useful, marked with // instead of our usual multiline style.
 */

internal enum WIC_LOADER_FLAGS : uint {
    WIC_LOADER_DEFAULT      = 0,
    WIC_LOADER_FORCE_SRGB   = 0x1,
    WIC_LOADER_IGNORE_SRGB  = 0x2,
    WIC_LOADER_SRGB_DEFAULT = 0x4,
    WIC_LOADER_FIT_POW2     = 0x20,
    WIC_LOADER_MAKE_SQUARE  = 0x40,
    WIC_LOADER_FORCE_RGBA32 = 0x80,
};

/// <summary>
///     Provides Windows Imaging Component (WIC) services.
/// </summary>
internal sealed unsafe class FhImaging {

    private static readonly Dictionary<Guid, DXGI_FORMAT> _wic_to_dxgi;
    private static readonly Dictionary<Guid, Guid>        _wic_to_nearest;

    private static ComPtr<IWICImagingFactory2> _IWICImagingFactory;
    private static int                         _init;

    static FhImaging() {
        // https://github.com/microsoft/DirectXTK/blob/873aaf3d1e658235bf7e3c153a7361911136b650/Src/WICTextureLoader.cpp#L210
        _wic_to_dxgi = new() {
            { GUID.GUID_WICPixelFormat128bppRGBAFloat,    DXGI_FORMAT.DXGI_FORMAT_R32G32B32A32_FLOAT         },
            { GUID.GUID_WICPixelFormat64bppRGBAHalf,      DXGI_FORMAT.DXGI_FORMAT_R16G16B16A16_FLOAT         },
            { GUID.GUID_WICPixelFormat64bppRGBA,          DXGI_FORMAT.DXGI_FORMAT_R16G16B16A16_UNORM         },
            { GUID.GUID_WICPixelFormat32bppRGBA,          DXGI_FORMAT.DXGI_FORMAT_R8G8B8A8_UNORM             },
            { GUID.GUID_WICPixelFormat32bppBGRA,          DXGI_FORMAT.DXGI_FORMAT_B8G8R8A8_UNORM             }, // DXGI 1.1
            { GUID.GUID_WICPixelFormat32bppBGR,           DXGI_FORMAT.DXGI_FORMAT_B8G8R8X8_UNORM             }, // DXGI 1.1
            { GUID.GUID_WICPixelFormat32bppRGBA1010102XR, DXGI_FORMAT.DXGI_FORMAT_R10G10B10_XR_BIAS_A2_UNORM }, // DXGI 1.1
            { GUID.GUID_WICPixelFormat32bppRGBA1010102,   DXGI_FORMAT.DXGI_FORMAT_R10G10B10A2_UNORM          },
            { GUID.GUID_WICPixelFormat16bppBGRA5551,      DXGI_FORMAT.DXGI_FORMAT_B5G5R5A1_UNORM             },
            { GUID.GUID_WICPixelFormat16bppBGR565,        DXGI_FORMAT.DXGI_FORMAT_B5G6R5_UNORM               },
            { GUID.GUID_WICPixelFormat32bppGrayFloat,     DXGI_FORMAT.DXGI_FORMAT_R32_FLOAT                  },
            { GUID.GUID_WICPixelFormat16bppGrayHalf,      DXGI_FORMAT.DXGI_FORMAT_R16_FLOAT                  },
            { GUID.GUID_WICPixelFormat16bppGray,          DXGI_FORMAT.DXGI_FORMAT_R16_UNORM                  },
            { GUID.GUID_WICPixelFormat8bppGray,           DXGI_FORMAT.DXGI_FORMAT_R8_UNORM                   },
            { GUID.GUID_WICPixelFormat8bppAlpha,          DXGI_FORMAT.DXGI_FORMAT_A8_UNORM                   },

            /* [fkelava 24/10/26 14:59]
             * Normally guarded by a check for WIC2. We assume we have access to it.
             */
            { GUID.GUID_WICPixelFormat96bppRGBFloat,      DXGI_FORMAT.DXGI_FORMAT_R32G32B32_FLOAT            },
        };

        _wic_to_nearest = new() {
            // Note target GUID in this conversion table must be one of those directly supported formats (above).

            { GUID.GUID_WICPixelFormatBlackWhite,            GUID.GUID_WICPixelFormat8bppGray         }, // DXGI_FORMAT_R8_UNORM
            { GUID.GUID_WICPixelFormat1bppIndexed,           GUID.GUID_WICPixelFormat32bppRGBA        }, // DXGI_FORMAT_R8G8B8A8_UNORM
            { GUID.GUID_WICPixelFormat2bppIndexed,           GUID.GUID_WICPixelFormat32bppRGBA        }, // DXGI_FORMAT_R8G8B8A8_UNORM
            { GUID.GUID_WICPixelFormat4bppIndexed,           GUID.GUID_WICPixelFormat32bppRGBA        }, // DXGI_FORMAT_R8G8B8A8_UNORM
            { GUID.GUID_WICPixelFormat8bppIndexed,           GUID.GUID_WICPixelFormat32bppRGBA        }, // DXGI_FORMAT_R8G8B8A8_UNORM
            { GUID.GUID_WICPixelFormat2bppGray,              GUID.GUID_WICPixelFormat8bppGray         }, // DXGI_FORMAT_R8_UNORM
            { GUID.GUID_WICPixelFormat4bppGray,              GUID.GUID_WICPixelFormat8bppGray         }, // DXGI_FORMAT_R8_UNORM
            { GUID.GUID_WICPixelFormat16bppGrayFixedPoint,   GUID.GUID_WICPixelFormat16bppGrayHalf    }, // DXGI_FORMAT_R16_FLOAT
            { GUID.GUID_WICPixelFormat32bppGrayFixedPoint,   GUID.GUID_WICPixelFormat32bppGrayFloat   }, // DXGI_FORMAT_R32_FLOAT
            { GUID.GUID_WICPixelFormat16bppBGR555,           GUID.GUID_WICPixelFormat16bppBGRA5551    }, // DXGI_FORMAT_B5G5R5A1_UNORM
            { GUID.GUID_WICPixelFormat32bppBGR101010,        GUID.GUID_WICPixelFormat32bppRGBA1010102 }, // DXGI_FORMAT_R10G10B10A2_UNORM
            { GUID.GUID_WICPixelFormat24bppBGR,              GUID.GUID_WICPixelFormat32bppRGBA        }, // DXGI_FORMAT_R8G8B8A8_UNORM
            { GUID.GUID_WICPixelFormat24bppRGB,              GUID.GUID_WICPixelFormat32bppRGBA        }, // DXGI_FORMAT_R8G8B8A8_UNORM
            { GUID.GUID_WICPixelFormat32bppPBGRA,            GUID.GUID_WICPixelFormat32bppRGBA        }, // DXGI_FORMAT_R8G8B8A8_UNORM
            { GUID.GUID_WICPixelFormat32bppPRGBA,            GUID.GUID_WICPixelFormat32bppRGBA        }, // DXGI_FORMAT_R8G8B8A8_UNORM
            { GUID.GUID_WICPixelFormat48bppRGB,              GUID.GUID_WICPixelFormat64bppRGBA        }, // DXGI_FORMAT_R16G16B16A16_UNORM
            { GUID.GUID_WICPixelFormat48bppBGR,              GUID.GUID_WICPixelFormat64bppRGBA        }, // DXGI_FORMAT_R16G16B16A16_UNORM
            { GUID.GUID_WICPixelFormat64bppBGRA,             GUID.GUID_WICPixelFormat64bppRGBA        }, // DXGI_FORMAT_R16G16B16A16_UNORM
            { GUID.GUID_WICPixelFormat64bppPRGBA,            GUID.GUID_WICPixelFormat64bppRGBA        }, // DXGI_FORMAT_R16G16B16A16_UNORM
            { GUID.GUID_WICPixelFormat64bppPBGRA,            GUID.GUID_WICPixelFormat64bppRGBA        }, // DXGI_FORMAT_R16G16B16A16_UNORM
            { GUID.GUID_WICPixelFormat48bppRGBFixedPoint,    GUID.GUID_WICPixelFormat64bppRGBAHalf    }, // DXGI_FORMAT_R16G16B16A16_FLOAT
            { GUID.GUID_WICPixelFormat48bppBGRFixedPoint,    GUID.GUID_WICPixelFormat64bppRGBAHalf    }, // DXGI_FORMAT_R16G16B16A16_FLOAT
            { GUID.GUID_WICPixelFormat64bppRGBAFixedPoint,   GUID.GUID_WICPixelFormat64bppRGBAHalf    }, // DXGI_FORMAT_R16G16B16A16_FLOAT
            { GUID.GUID_WICPixelFormat64bppBGRAFixedPoint,   GUID.GUID_WICPixelFormat64bppRGBAHalf    }, // DXGI_FORMAT_R16G16B16A16_FLOAT
            { GUID.GUID_WICPixelFormat64bppRGBFixedPoint,    GUID.GUID_WICPixelFormat64bppRGBAHalf    }, // DXGI_FORMAT_R16G16B16A16_FLOAT
            { GUID.GUID_WICPixelFormat64bppRGBHalf,          GUID.GUID_WICPixelFormat64bppRGBAHalf    }, // DXGI_FORMAT_R16G16B16A16_FLOAT
            { GUID.GUID_WICPixelFormat48bppRGBHalf,          GUID.GUID_WICPixelFormat64bppRGBAHalf    }, // DXGI_FORMAT_R16G16B16A16_FLOAT
            { GUID.GUID_WICPixelFormat128bppPRGBAFloat,      GUID.GUID_WICPixelFormat128bppRGBAFloat  }, // DXGI_FORMAT_R32G32B32A32_FLOAT
            { GUID.GUID_WICPixelFormat128bppRGBFloat,        GUID.GUID_WICPixelFormat128bppRGBAFloat  }, // DXGI_FORMAT_R32G32B32A32_FLOAT
            { GUID.GUID_WICPixelFormat128bppRGBAFixedPoint,  GUID.GUID_WICPixelFormat128bppRGBAFloat  }, // DXGI_FORMAT_R32G32B32A32_FLOAT
            { GUID.GUID_WICPixelFormat128bppRGBFixedPoint,   GUID.GUID_WICPixelFormat128bppRGBAFloat  }, // DXGI_FORMAT_R32G32B32A32_FLOAT
            { GUID.GUID_WICPixelFormat32bppRGBE,             GUID.GUID_WICPixelFormat128bppRGBAFloat  }, // DXGI_FORMAT_R32G32B32A32_FLOAT
            { GUID.GUID_WICPixelFormat32bppCMYK,             GUID.GUID_WICPixelFormat32bppRGBA        }, // DXGI_FORMAT_R8G8B8A8_UNORM
            { GUID.GUID_WICPixelFormat64bppCMYK,             GUID.GUID_WICPixelFormat64bppRGBA        }, // DXGI_FORMAT_R16G16B16A16_UNORM
            { GUID.GUID_WICPixelFormat40bppCMYKAlpha,        GUID.GUID_WICPixelFormat32bppRGBA        }, // DXGI_FORMAT_R8G8B8A8_UNORM
            { GUID.GUID_WICPixelFormat80bppCMYKAlpha,        GUID.GUID_WICPixelFormat64bppRGBA        }, // DXGI_FORMAT_R16G16B16A16_UNORM
            { GUID.GUID_WICPixelFormat32bppRGB,              GUID.GUID_WICPixelFormat32bppRGBA        }, // DXGI_FORMAT_R8G8B8A8_UNORM
            { GUID.GUID_WICPixelFormat64bppRGB,              GUID.GUID_WICPixelFormat64bppRGBA        }, // DXGI_FORMAT_R16G16B16A16_UNORM
            { GUID.GUID_WICPixelFormat64bppPRGBAHalf,        GUID.GUID_WICPixelFormat64bppRGBAHalf    }, // DXGI_FORMAT_R16G16B16A16_FLOAT

            // We don't support n-channel formats
        };
    }

    // https://github.com/microsoft/DirectXTK/blob/873aaf3d1e658235bf7e3c153a7361911136b650/Src/LoaderHelpers.h#L193
    private static DXGI_FORMAT MakeSRGB(DXGI_FORMAT format) {
        return format switch {
            DXGI_FORMAT.DXGI_FORMAT_R8G8B8A8_UNORM => DXGI_FORMAT.DXGI_FORMAT_R8G8B8A8_UNORM_SRGB,
            DXGI_FORMAT.DXGI_FORMAT_BC1_UNORM      => DXGI_FORMAT.DXGI_FORMAT_BC1_UNORM_SRGB,
            DXGI_FORMAT.DXGI_FORMAT_BC2_UNORM      => DXGI_FORMAT.DXGI_FORMAT_BC2_UNORM_SRGB,
            DXGI_FORMAT.DXGI_FORMAT_BC3_UNORM      => DXGI_FORMAT.DXGI_FORMAT_BC3_UNORM_SRGB,
            DXGI_FORMAT.DXGI_FORMAT_B8G8R8A8_UNORM => DXGI_FORMAT.DXGI_FORMAT_B8G8R8A8_UNORM_SRGB,
            DXGI_FORMAT.DXGI_FORMAT_B8G8R8X8_UNORM => DXGI_FORMAT.DXGI_FORMAT_B8G8R8X8_UNORM_SRGB,
            DXGI_FORMAT.DXGI_FORMAT_BC7_UNORM      => DXGI_FORMAT.DXGI_FORMAT_BC7_UNORM_SRGB,
            _                                      => format,
        };
    }

    // https://github.com/microsoft/DirectXTK/blob/873aaf3d1e658235bf7e3c153a7361911136b650/Src/WICTextureLoader.cpp#L155
    // https://github.com/microsoft/DirectXTK/blob/873aaf3d1e658235bf7e3c153a7361911136b650/Src/WICTextureLoader.cpp#L192
    private static IWICImagingFactory2* GetWIC() {
        if (Interlocked.CompareExchange(ref _init, 1, 0) == 1)
            return _IWICImagingFactory.Get();

        using ComPtr<IWICImagingFactory2> com_ptr_factory = default;

        HRESULT hr;
        fixed (Guid* ptr_clsid = &CLSID.CLSID_WICImagingFactory2) {
            hr = Windows.CoCreateInstance(
                ptr_clsid,
                null,
                (uint) CLSCTX.CLSCTX_INPROC_SERVER,
                Windows.__uuidof<IWICImagingFactory2>(),
                (void**) com_ptr_factory.GetAddressOf()
            );

            if (hr.SUCCEEDED) {
                com_ptr_factory.Swap(ref _IWICImagingFactory);
            }
        }

        return _IWICImagingFactory.Get();
    }

    // https://github.com/microsoft/DirectXTK/blob/873aaf3d1e658235bf7e3c153a7361911136b650/Src/WICTextureLoader.cpp#L228
    private static nuint WICBitsPerPixel(Guid target) {
        uint bpp = 0;

        IWICImagingFactory2* ptr_factory = GetWIC();
        if (ptr_factory == null)
            return bpp;

        using ComPtr<IWICComponentInfo>   cinfo  = default;
        using ComPtr<IWICPixelFormatInfo> pfinfo = default;

        HRESULT hr = ptr_factory->CreateComponentInfo(&target, cinfo.GetAddressOf());
        if (hr.FAILED)
            return bpp;

        IWICComponentInfo* ptr_cinfo = cinfo.Get();
        WICComponentType   type;

        hr = ptr_cinfo->GetComponentType(&type);
        if (hr.FAILED)
            return bpp;

        if (type != WICComponentType.WICPixelFormat)
            return bpp;

        hr = cinfo.As(&pfinfo);
        if (hr.FAILED)
            return bpp;

        hr = pfinfo.Get()->GetBitsPerPixel(&bpp);
        if (hr.FAILED)
            return bpp;

        return bpp;
    }

    /* [fkelava 24/10/26 15:29]
     * This is utterly illegible and has five indent levels in the original,
     * so I split out the SRGB handling segment of CreateTextureFromWIC here.
     */

    // https://github.com/microsoft/DirectXTK/blob/873aaf3d1e658235bf7e3c153a7361911136b650/Src/WICTextureLoader.cpp#L426
    private static DXGI_FORMAT _HANDLE_SRGB_FORMATS(
        IWICBitmapFrameDecode* ptr_frame_decoder,
        WIC_LOADER_FLAGS       flags_load,
        DXGI_FORMAT            format
    ) {
        if (flags_load.HasFlag(WIC_LOADER_FLAGS.WIC_LOADER_FORCE_SRGB))
            return MakeSRGB(format);

        if (flags_load.HasFlag(WIC_LOADER_FLAGS.WIC_LOADER_IGNORE_SRGB))
            return format;

        const string meta_png_srgb = "/sRGB/RenderingIntent";
        const string meta_png_gama = "/gAMA/ImageGamma";
        const string meta_gen_srgb = "System.Image.ColorSpace";

        using ComPtr<IWICMetadataQueryReader> metareader = default;

        HRESULT hr;
        Guid    format_container;

        hr = ptr_frame_decoder->GetMetadataQueryReader(metareader.GetAddressOf());
        if (!hr.SUCCEEDED)
            return format;

        hr = metareader.Get()->GetContainerFormat(&format_container);
        if (!hr.SUCCEEDED)
            return format;

        PROPVARIANT value;
        Windows.PropVariantInit(&value);

        if (format_container == GUID.GUID_ContainerFormatPng) {
            fixed (char* ptr_meta_png_srgb = meta_png_srgb)
            fixed (char* ptr_meta_png_gama = meta_png_gama
            ) {
                hr = metareader.Get()->GetMetadataByName(ptr_meta_png_srgb, &value);
                if (hr.SUCCEEDED && value.vt == (ushort) VARENUM.VT_UI1)
                    return MakeSRGB(format);

                hr = metareader.Get()->GetMetadataByName(ptr_meta_png_gama, &value);
                if (hr.SUCCEEDED && value.vt == (ushort) VARENUM.VT_UI4 && value.uintVal == 45455)
                    return MakeSRGB(format);
            }
        }

        fixed (char* ptr_meta_gen_srgb = meta_gen_srgb) {
            hr = metareader.Get()->GetMetadataByName(ptr_meta_gen_srgb, &value);
            if (hr.SUCCEEDED && value.vt == (ushort) VARENUM.VT_UI2 && value.uiVal == 1)
                return MakeSRGB(format);
        }

        _ = Windows.PropVariantClear(&value);

        return flags_load.HasFlag(WIC_LOADER_FLAGS.WIC_LOADER_SRGB_DEFAULT)
            ? MakeSRGB(format)
            : format;
    }

    // https://github.com/microsoft/DirectXTK/blob/873aaf3d1e658235bf7e3c153a7361911136b650/Src/WICTextureLoader.cpp#L257
    private static HRESULT CreateTextureFromWIC(
        ID3D11Device*              ptr_device,
        ID3D11DeviceContext*       ptr_device_context,
        IWICBitmapFrameDecode*     ptr_frame_decoder,
        nuint                      maxsize,
        D3D11_USAGE                usage,
        D3D11_BIND_FLAG            flags_bind,
        D3D11_CPU_ACCESS_FLAG      flags_cpu_access,
        D3D11_RESOURCE_MISC_FLAG   flags_misc,
        WIC_LOADER_FLAGS           flags_load,
        ID3D11Resource**           pp_texture,
        ID3D11ShaderResourceView** pp_srv
    ) {
        uint    width, height;
        HRESULT hr = ptr_frame_decoder->GetSize(&width, &height);

        if (hr.FAILED)
            return hr;

        if (maxsize > uint.MaxValue)
            return E.E_INVALIDARG;

        Trace.Assert(width > 0 && height > 0);

        if (maxsize == 0) {
            maxsize = ptr_device->GetFeatureLevel() switch {
                D3D_FEATURE_LEVEL.D3D_FEATURE_LEVEL_9_1  or
                D3D_FEATURE_LEVEL.D3D_FEATURE_LEVEL_9_2  or
                D3D_FEATURE_LEVEL.D3D_FEATURE_LEVEL_9_3  => 0,
                D3D_FEATURE_LEVEL.D3D_FEATURE_LEVEL_10_0 or
                D3D_FEATURE_LEVEL.D3D_FEATURE_LEVEL_10_1 => D3D10.D3D10_REQ_TEXTURE2D_U_OR_V_DIMENSION,
                _                                        => D3D11.D3D11_REQ_TEXTURE2D_U_OR_V_DIMENSION
            };
        }

        Trace.Assert(maxsize > 0);

        uint twidth  = width;
        uint theight = height;
        if (flags_load.HasFlag(WIC_LOADER_FLAGS.WIC_LOADER_FIT_POW2)) {
            // NO-OP
        }
        else if (width > maxsize || height > maxsize) {
            float aspect_ratio = height / width;

            if (width > height) {
                twidth  = uint.CreateChecked(maxsize);
                theight = uint.Max(1, uint.CreateChecked(maxsize * aspect_ratio));
            }
            else {
                theight = uint.CreateChecked(maxsize);
                twidth  = uint.Max(1, uint.CreateChecked(maxsize / aspect_ratio));
            }

            Trace.Assert(twidth <= maxsize && theight <= maxsize);
        }

        if (flags_load.HasFlag(WIC_LOADER_FLAGS.WIC_LOADER_MAKE_SQUARE)) {
            twidth  = uint.Max(twidth, theight);
            theight = twidth;
        }

        Guid format_pixel;
        hr = ptr_frame_decoder->GetPixelFormat(&format_pixel);

        if (hr.FAILED)
            return hr;

        Guid format_convert = format_pixel;

        nuint bpp = 0;

        /* [fkelava 24/10/26 14:49]
         * Here we simplify a bit because we assume WIC2 mode and fold
         * any relevant edge cases straight into the dictionaries.
         */
        if (!_wic_to_dxgi.TryGetValue(format_pixel, out DXGI_FORMAT format)) {
            if (format_pixel == GUID.GUID_WICPixelFormat96bppRGBFixedPoint) {
                format_convert = GUID.GUID_WICPixelFormat96bppRGBFloat;
                format         = DXGI_FORMAT.DXGI_FORMAT_R32G32B32_FLOAT;
                bpp            = 96;
            }
            else {
                if (!_wic_to_nearest.TryGetValue(format_pixel,         out Guid format_pixel_nearest)
                || (!_wic_to_dxgi   .TryGetValue(format_pixel_nearest, out      format))) {
                    FhInternal.Log.Error($"Unsupported texture pixel format {format_pixel}");
                    return Windows.HRESULT_FROM_WIN32(ERROR.ERROR_NOT_SUPPORTED);
                }

                format_convert = format_pixel_nearest;
                bpp            = WICBitsPerPixel(format_convert);
            }
        }
        else {
            bpp = WICBitsPerPixel(format_pixel);
        }

        if (format             == DXGI_FORMAT.DXGI_FORMAT_R32G32B32A32_FLOAT
        &&  ptr_device_context != null
        &&  pp_srv             != null
        ) {
            // Special case test for optional device support for autogen mipchains for R32G32B32_FLOAT
            uint format_supported;
            hr = ptr_device->CheckFormatSupport(DXGI_FORMAT.DXGI_FORMAT_R32G32B32_FLOAT, &format_supported);

            if (hr.FAILED || (format_supported & (uint)D3D11_FORMAT_SUPPORT.D3D11_FORMAT_SUPPORT_MIP_AUTOGEN) == 0) {
                // Use R32G32B32A32_FLOAT instead which is required for Feature Level 10.0 and up
                format_convert = GUID.GUID_WICPixelFormat128bppRGBAFloat;
                format         = DXGI_FORMAT.DXGI_FORMAT_R32G32B32A32_FLOAT;
                bpp            = 128;
            }
        }

        if (flags_load.HasFlag(WIC_LOADER_FLAGS.WIC_LOADER_FORCE_RGBA32)) {
            format_convert = GUID.GUID_WICPixelFormat32bppRGBA;
            format         = DXGI_FORMAT.DXGI_FORMAT_R8G8B8A8_UNORM;
            bpp            = 32;
        }

        if (bpp == 0)
            return E.E_FAIL;

        // Handle sRGB formats
        format = _HANDLE_SRGB_FORMATS(
            ptr_frame_decoder,
            flags_load,
            format
        );

        // Verify our target format is supported by the current device
        // (handles WDDM 1.0 or WDDM 1.1 device driver cases as well as DirectX 11.0 Runtime without 16bpp format support)
        uint support = 0;
        hr = ptr_device->CheckFormatSupport(format, &support);

        if (hr.FAILED || (support & (uint)D3D11_FORMAT_SUPPORT.D3D11_FORMAT_SUPPORT_TEXTURE2D) == 0) {
            // Fallback to RGBA 32-bit format which is supported by all devices
            format_convert = GUID.GUID_WICPixelFormat32bppRGBA;
            format         = DXGI_FORMAT.DXGI_FORMAT_R8G8B8A8_UNORM;
            bpp            = 32;
        }

        // Allocate temporary memory for image
        ulong bytes_row = (twidth * bpp + 7) / 8;
        ulong bytes_num = bytes_row * theight;

        if (bytes_row > uint.MaxValue || bytes_num > uint.MaxValue)
            return Windows.HRESULT_FROM_WIN32(ERROR.ERROR_ARITHMETIC_OVERFLOW);

        uint row_pitch  = uint.CreateSaturating(bytes_row);
        uint image_size = uint.CreateSaturating(bytes_num);

        byte[] temp = new byte[image_size];

        if (format_convert == format_pixel
        &&  twidth         == width
        &&  theight        == height
        ) {
            fixed (byte* ptr_temp = temp) {
                // No format conversion or resize needed
                hr = ptr_frame_decoder->CopyPixels(null, row_pitch, image_size, ptr_temp);

                if (hr.FAILED)
                    return hr;
            }
        }
        else if (twidth != width || theight != height) {
            // Resize
            IWICImagingFactory2* ptr_factory = GetWIC();
            if (ptr_factory == null)
                return E.E_NOINTERFACE;

            using ComPtr<IWICBitmapScaler> scaler = default;
            hr = ptr_factory->CreateBitmapScaler(scaler.GetAddressOf());

            if (hr.FAILED)
                return hr;

            hr = scaler.Get()->Initialize(
                (IWICBitmapSource*) ptr_frame_decoder,
                twidth,
                theight,
                WICBitmapInterpolationMode.WICBitmapInterpolationModeFant
            );

            if (hr.FAILED)
                return hr;

            Guid format_scaler;
            hr = scaler.Get()->GetPixelFormat(&format_scaler);

            if (hr.FAILED)
                return hr;

            if (format_convert == format_scaler) {
                // No format conversion required.
                fixed (byte* ptr_temp = temp) {
                    hr = scaler.Get()->CopyPixels(null, row_pitch, image_size, ptr_temp);

                    if (hr.FAILED)
                        return hr;
                }
            }
            else {
                using ComPtr<IWICFormatConverter> converter = default;
                hr = ptr_factory->CreateFormatConverter(converter.GetAddressOf());

                if (hr.FAILED)
                    return hr;

                BOOL can_convert = 0;
                hr = converter.Get()->CanConvert(&format_scaler, &format_convert, &can_convert);

                if (hr.FAILED || can_convert == 0)
                    return E.E_UNEXPECTED;

                hr = converter.Get()->Initialize(
                    (IWICBitmapSource*) scaler.Get(),
                    &format_convert,
                    WICBitmapDitherType.WICBitmapDitherTypeErrorDiffusion,
                    null,
                    0,
                    WICBitmapPaletteType.WICBitmapPaletteTypeMedianCut
                );

                if (hr.FAILED)
                    return hr;

                fixed (byte* ptr_temp = temp) {
                    hr = converter.Get()->CopyPixels(null, row_pitch, image_size, ptr_temp);

                    if (hr.FAILED)
                        return hr;
                }
            }
        }
        else {
            // Format conversion, but no resize
            IWICImagingFactory2* ptr_factory = GetWIC();
            if (ptr_factory == null)
                return E.E_NOINTERFACE;

            using ComPtr<IWICFormatConverter> converter = default;
            hr = ptr_factory->CreateFormatConverter(converter.GetAddressOf());

            if (hr.FAILED)
                return hr;

            BOOL can_convert = 0;
            hr = converter.Get()->CanConvert(&format_pixel, &format_convert, &can_convert);

            if (hr.FAILED || can_convert == 0)
                return E.E_UNEXPECTED;

            hr = converter.Get()->Initialize(
                (IWICBitmapSource*) ptr_frame_decoder,
                &format_convert,
                WICBitmapDitherType.WICBitmapDitherTypeErrorDiffusion,
                null,
                0,
                WICBitmapPaletteType.WICBitmapPaletteTypeMedianCut
            );

            if (hr.FAILED)
                return hr;

            fixed (byte* ptr_temp = temp) {
                hr = converter.Get()->CopyPixels(null, row_pitch, image_size, ptr_temp);

                if (hr.FAILED)
                    return hr;
            }
        }

        // See if format is supported for auto-gen mipmaps (varies by feature level)
        bool autogen = false;

        if (ptr_device_context != null
        &&  pp_srv             != null
        ) { // Must have context and shader-view to auto generate mipmaps
            uint format_support = 0;
            hr = ptr_device->CheckFormatSupport(format, &format_support);

            autogen = hr.SUCCEEDED && (format_support & (uint)D3D11_FORMAT_SUPPORT.D3D11_FORMAT_SUPPORT_MIP_AUTOGEN) != 0;
        }

        D3D11_TEXTURE2D_DESC desc;
        desc.Width                = twidth;
        desc.Height               = theight;
        desc.MipLevels            = (autogen) ? 0U : 1U;
        desc.ArraySize            = 1;
        desc.Format               = format;
        desc.Usage                = usage;
        desc.CPUAccessFlags       = (uint) flags_cpu_access;
        desc.SampleDesc.Count     = 1;
        desc.SampleDesc.Quality   = 0;

        desc.BindFlags = (uint)(autogen
            ? flags_bind | D3D11_BIND_FLAG.D3D11_BIND_SHADER_RESOURCE | D3D11_BIND_FLAG.D3D11_BIND_RENDER_TARGET
            : flags_bind);
        desc.MiscFlags = (uint)(autogen
            ? flags_misc | D3D11_RESOURCE_MISC_FLAG.D3D11_RESOURCE_MISC_GENERATE_MIPS
            : flags_misc);

        D3D11_SUBRESOURCE_DATA init_data;
        ID3D11Texture2D*       tex;

        fixed (byte* ptr_temp = temp) {
            init_data.pSysMem          = ptr_temp;
            init_data.SysMemPitch      = row_pitch;
            init_data.SysMemSlicePitch = image_size;

            hr = ptr_device->CreateTexture2D(
                &desc,
                autogen ? null : &init_data,
                &tex
            );

            if (hr.FAILED || tex == null)
                return hr;
        }

        if (pp_srv == null) {
            if (pp_texture != null) {
                *pp_texture = (ID3D11Resource*) tex;
            }
            /* [fkelava 24/10/26 16:25]
             * SetDebugObjectName elided.
             */
            else tex->Release();

            return hr;
        }

        D3D11_SHADER_RESOURCE_VIEW_DESC srv_desc = new();
        srv_desc.Format              = desc.Format;
        srv_desc.ViewDimension       = D3D_SRV_DIMENSION.D3D11_SRV_DIMENSION_TEXTURE2D;
        srv_desc.Texture2D.MipLevels = autogen ? uint.MaxValue : 1U;

        hr = ptr_device->CreateShaderResourceView(
            (ID3D11Resource*) tex,
            &srv_desc,
            pp_srv
        );

        if (hr.FAILED) {
            tex->Release();
            return hr;
        }

        if (autogen) {
            Trace.Assert(ptr_device_context != null);

            fixed (byte* ptr_temp = temp) {
                ptr_device_context->UpdateSubresource(
                    (ID3D11Resource*) tex,
                    0,
                    null,
                    ptr_temp,
                    row_pitch,
                    image_size
                );

                ptr_device_context->GenerateMips(*pp_srv);
            }
        }

        if (pp_texture != null) {
            *pp_texture = (ID3D11Resource*) tex;
        }
        /* [fkelava 24/10/26 16:25]
         * SetDebugObjectName elided.
         */
        else tex->Release();

        return hr;
    }

    /* [fkelava 24/10/26 16:57]
     * --- PUBLIC FUNCTIONS HERE ---
     *
     * Overloads without a ID3D11DeviceContext* are ELIDED for sanity.
     * Consumers of this API will always have a device context available.
     */

    // https://github.com/microsoft/DirectXTK/blob/873aaf3d1e658235bf7e3c153a7361911136b650/Src/WICTextureLoader.cpp#L793
    public static HRESULT CreateWICTextureFromMemory(
        ID3D11Device*              ptr_device,
        ID3D11DeviceContext*       ptr_device_context,
        byte*                      ptr_texbuf,
        nuint                      size_texbuf,
        nuint                      maxsize,
        ID3D11Resource**           pp_texture,
        ID3D11ShaderResourceView** pp_srv
    ) {
        /* [fkelava 24/10/26 19:04]
         * We have to suppress sRGB processing/promotion, or the texture will be off-color.
         * See https://github.com/microsoft/DirectXTK/issues/156.
         */
        return CreateWICTextureFromMemoryEx(
            ptr_device,
            ptr_device_context,
            ptr_texbuf,
            size_texbuf,
            maxsize,
            D3D11_USAGE.D3D11_USAGE_DEFAULT,
            D3D11_BIND_FLAG.D3D11_BIND_SHADER_RESOURCE,
            0,
            0,
            WIC_LOADER_FLAGS.WIC_LOADER_IGNORE_SRGB,
            pp_texture,
            pp_srv
        );
    }

    // https://github.com/microsoft/DirectXTK/blob/873aaf3d1e658235bf7e3c153a7361911136b650/Src/WICTextureLoader.cpp#L914
    public static HRESULT CreateWICTextureFromMemoryEx(
        ID3D11Device*              ptr_device,
        ID3D11DeviceContext*       ptr_device_context,
        byte*                      ptr_texbuf,
        nuint                      size_texbuf,
        nuint                      maxsize,
        D3D11_USAGE                usage,
        D3D11_BIND_FLAG            flags_bind,
        D3D11_CPU_ACCESS_FLAG      flags_cpu_access,
        D3D11_RESOURCE_MISC_FLAG   flags_misc,
        WIC_LOADER_FLAGS           flags_load,
        ID3D11Resource**           pp_texture,
        ID3D11ShaderResourceView** pp_srv
    ) {
        if (pp_texture != null) *pp_texture = null;
        if (pp_srv     != null) *pp_srv     = null;

        if (ptr_device == null || (pp_srv == null && pp_texture == null))
            return E.E_INVALIDARG;

        if (pp_srv != null && !flags_bind.HasFlag(D3D11_BIND_FLAG.D3D11_BIND_SHADER_RESOURCE))
            return E.E_INVALIDARG;

        if (size_texbuf == 0)             return E.E_FAIL;
        if (size_texbuf >  uint.MaxValue) return Windows.HRESULT_FROM_WIN32(ERROR.ERROR_FILE_TOO_LARGE);

        IWICImagingFactory2* ptr_factory = GetWIC();
        if (ptr_factory == null)
            return E.E_NOINTERFACE;

        // Initialize WIC
        using ComPtr<IWICStream>            stream        = default;
        using ComPtr<IWICBitmapDecoder>     decoder       = default;
        using ComPtr<IWICBitmapFrameDecode> frame_decoder = default;

        HRESULT hr = ptr_factory->CreateStream(stream.GetAddressOf());

        if (hr.FAILED)
            return hr;

        hr = stream.Get()->InitializeFromMemory(ptr_texbuf, uint.CreateChecked(size_texbuf));

        if (hr.FAILED)
            return hr;

        hr = ptr_factory->CreateDecoderFromStream(
            (IStream*) stream.Get(),
            null,
            WICDecodeOptions.WICDecodeMetadataCacheOnDemand,
            decoder.GetAddressOf()
        );

        if (hr.FAILED)
            return hr;

        hr = decoder.Get()->GetFrame(0, frame_decoder.GetAddressOf());

        if (hr.FAILED)
            return hr;

        hr = CreateTextureFromWIC(
            ptr_device,
            ptr_device_context,
            frame_decoder.Get(),
            maxsize,
            usage,
            flags_bind,
            flags_cpu_access,
            flags_misc,
            flags_load,
            pp_texture,
            pp_srv
        );

        /* [fkelava 24/10/26 16:42]
         * SetDebugTextureInfo elided.
         */

        return hr;
    }

    // https://github.com/microsoft/DirectXTK/blob/873aaf3d1e658235bf7e3c153a7361911136b650/Src/WICTextureLoader.cpp#L1033
    public static HRESULT CreateWICTextureFromFile(
        ID3D11Device*              ptr_device,
        ID3D11DeviceContext*       ptr_device_context,
        string                     file_name,
        nuint                      maxsize,
        ID3D11Resource**           pp_texture,
        ID3D11ShaderResourceView** pp_srv
    ) {
        /* [fkelava 24/10/26 19:04]
         * We have to suppress sRGB processing/promotion, or the texture will be off-color.
         * See https://github.com/microsoft/DirectXTK/issues/156.
         */
        return CreateWICTextureFromFileEx(
            ptr_device,
            ptr_device_context,
            file_name,
            maxsize,
            D3D11_USAGE.D3D11_USAGE_DEFAULT,
            D3D11_BIND_FLAG.D3D11_BIND_SHADER_RESOURCE,
            0,
            0,
            WIC_LOADER_FLAGS.WIC_LOADER_IGNORE_SRGB,
            pp_texture,
            pp_srv
        );
    }

    // https://github.com/microsoft/DirectXTK/blob/873aaf3d1e658235bf7e3c153a7361911136b650/Src/WICTextureLoader.cpp#L1128
    public static HRESULT CreateWICTextureFromFileEx(
        ID3D11Device*              ptr_device,
        ID3D11DeviceContext*       ptr_device_context,
        string                     file_name,
        nuint                      maxsize,
        D3D11_USAGE                usage,
        D3D11_BIND_FLAG            flags_bind,
        D3D11_CPU_ACCESS_FLAG      flags_cpu_access,
        D3D11_RESOURCE_MISC_FLAG   flags_misc,
        WIC_LOADER_FLAGS           flags_load,
        ID3D11Resource**           pp_texture,
        ID3D11ShaderResourceView** pp_srv
    ) {
        if (pp_texture != null) *pp_texture = null;
        if (pp_srv     != null) *pp_srv     = null;

        if (ptr_device == null || (pp_srv == null && pp_texture == null))
            return E.E_INVALIDARG;

        if (pp_srv != null && !flags_bind.HasFlag(D3D11_BIND_FLAG.D3D11_BIND_SHADER_RESOURCE))
            return E.E_INVALIDARG;

        IWICImagingFactory2* ptr_factory = GetWIC();
        if (ptr_factory == null)
            return E.E_NOINTERFACE;

        // Initialize WIC
        using ComPtr<IWICBitmapDecoder>     decoder       = default;
        using ComPtr<IWICBitmapFrameDecode> frame_decoder = default;

        HRESULT hr;

        fixed (char* ptr_file_name = file_name) {
            hr = ptr_factory->CreateDecoderFromFilename(
                ptr_file_name,
                null,
                Windows.GENERIC_READ,
                WICDecodeOptions.WICDecodeMetadataCacheOnDemand,
                decoder.GetAddressOf()
            );

            if (hr.FAILED)
                return hr;
        }

        hr = decoder.Get()->GetFrame(0, frame_decoder.GetAddressOf());

        if (hr.FAILED)
            return hr;

        hr = CreateTextureFromWIC(
            ptr_device,
            ptr_device_context,
            frame_decoder.Get(),
            maxsize,
            usage,
            flags_bind,
            flags_cpu_access,
            flags_misc,
            flags_load,
            pp_texture,
            pp_srv
        );

        /* [fkelava 24/10/26 16:42]
         * SetDebugTextureInfo elided.
         */

        return hr;
    }

}
