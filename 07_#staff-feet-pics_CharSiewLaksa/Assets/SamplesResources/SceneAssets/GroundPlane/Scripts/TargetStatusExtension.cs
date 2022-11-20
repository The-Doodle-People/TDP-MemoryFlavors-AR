/*==============================================================================
Copyright (c) 2021 PTC Inc. All Rights Reserved.

Vuforia is a trademark of PTC Inc., registered in the United States and other
countries.
==============================================================================*/
using Vuforia;

/// <summary>
/// Extension class to query target status information.
/// </summary>
public static class TargetStatusExtension
{
    /// <summary>
    /// More Strict: returns true when Status is Tracked and StatusInfo is Normal.
    /// </summary>
    public static bool IsTrackedAndNormal(this TargetStatus targetStatus)
    {
        return (targetStatus.Status == Status.TRACKED ||
                targetStatus.Status == Status.EXTENDED_TRACKED) &&
               targetStatus.StatusInfo == StatusInfo.NORMAL;
    }

    /// <summary>
    /// Less Strict: returns true when Status is Tracked/Normal or Limited/Unknown.
    /// </summary>
    public static bool IsTrackedOrLimited(this TargetStatus targetStatus)
    {
        return (targetStatus.Status == Status.TRACKED ||
                targetStatus.Status == Status.EXTENDED_TRACKED) &&
               targetStatus.StatusInfo == StatusInfo.NORMAL ||
               targetStatus.Status == Status.LIMITED && targetStatus.StatusInfo == StatusInfo.UNKNOWN;
    }
}
