/*===============================================================================
Copyright 2022 PTC Inc.

Licensed under the Apache License, Version 2.0 (the "License"); you may not
use this file except in compliance with the License. You may obtain a copy of
the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software distributed
under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR
CONDITIONS OF ANY KIND, either express or implied. See the License for the
specific language governing permissions and limitations under the License.
===============================================================================*/

#ifndef VUFORIA_VFX_INCLUDED
#define VUFORIA_VFX_INCLUDED

void axial_delta(float3 p, float3 center, float3 axis, float scale, float min, float max, out float deltaMin, out float deltaMax)
{
    float3 centerToPoint = p - center;
    float proj = dot(centerToPoint, normalize(axis)) / scale;
    deltaMin = proj - min;
    deltaMax = proj - max;
}

#endif //VUFORIA_VFX_INCLUDED