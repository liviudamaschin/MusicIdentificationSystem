using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateFingerprint
{
    public static class Error
    {
        public static string ManageError(Exception ex)
        {
            if (ex != null)
            {
                if (ex.InnerException != null)
                    return ManageError(ex.InnerException);
                else
                    return ex.Message;
            }
            else
                return string.Empty;
        }
    }
}
