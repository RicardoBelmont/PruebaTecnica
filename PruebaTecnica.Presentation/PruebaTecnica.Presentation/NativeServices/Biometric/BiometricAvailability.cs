using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnica.Presentation.NativeServices.Biometric
{
    public enum BiometricAvailability
    {
        Available,
        NoHardware,
        NoSecuredDevice,
        NoFingerprints,
        NoPermission,
        NoOSVersionSupported,
        Lockout,
        Unknown
    }
}
