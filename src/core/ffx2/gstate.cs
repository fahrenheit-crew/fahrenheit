// SPDX-License-Identifier: LGPL-3.0-or-later
//
// This file is part of Fahrenheit, © 2023-2026 The Fahrenheit contributors.
// It is licensed to you under the GNU Lesser General Public License, version 3.0 or later. See COPYING, COPYING.LESSER.

using Fahrenheit.Atel;

namespace Fahrenheit.FFX2;

public unsafe static class Globals {
    public static class Atel {
        // TODO: Needs updating for P4
        // public static int*                  request_count      => FhUtil.ptr_at<int>        (0xA116C4);
        // public static AtelRequest*          request_list       => FhUtil.ptr_at<AtelRequest>(0xA12EC8);
        public static AtelWorkerController* controllers        => FhUtil.ptr_at<AtelWorkerController>(0xD924D8);
        // public static AtelBasicWorker*      current_worker     => FhUtil.ptr_at<AtelBasicWorker>(0xD94AB0);
        public static AtelWorkerController* current_controller => (AtelWorkerController*)FhUtil.get_at<nint>(0xD924D4);
    }

    //public static Btl* btl => FhUtil.ptr_at<Btl>(0x9F77A0); // Maybe?
    public static SaveData* save_data => FhUtil.ptr_at<SaveData>(0x9F8510);

    public static int* event_id => FhUtil.ptr_at<int>(0x96174C);
}
