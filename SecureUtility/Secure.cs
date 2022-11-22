using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Text;

namespace Todolistapplication.SecureUtility;

public static class Secure
{
    private static readonly object SecurityLock = new object();

    private static readonly SecureString PasswordPrefix = "Y92PQ8Z227JHKRU20TTU".Convert();

    private static SecureString Convert(this string password)
    {
        lock (SecurityLock)
        {
            SecureString secureString = new SecureString();
            foreach (char c in password)
                secureString.AppendChar(c);
            secureString.MakeReadOnly();
            return secureString;
        }
    }

    public static byte[] GetPasswordHash(this string password)
    {
        if (password == null)
            throw new ArgumentNullException(nameof(password));
        return !string.IsNullOrWhiteSpace(password)
            ? password.ToSecure().GetPasswordHash()
            : throw new ArgumentOutOfRangeException(nameof(password), "Password is null or empty");
    }

    private static SecureString ToSecure(this string password)
    {
        if (password == null)
            throw new ArgumentNullException(nameof(password));
        if (string.IsNullOrWhiteSpace(password))
            throw new ArgumentOutOfRangeException(nameof(password), "Password is null or empty");
        lock (SecurityLock)
        {
            SecureString secure = Secure.PasswordPrefix.Copy();
            foreach (char c in password)
                secure.AppendChar(c);
            secure.MakeReadOnly();
            return secure;
        }
    }

    private static byte[] GetPasswordHash(this SecureString password) => password != null
        ? password.GetString().Hash256()
        : throw new ArgumentNullException(nameof(password));

    private static byte[] Hash256(this string data, Encoding encoding = null)
    {
        if (data == null)
            throw new ArgumentNullException(nameof(data));
        if (encoding == null)
            encoding = Encoding.UTF8;
        return Hash256(encoding.GetBytes(data));
    }

    private static byte[] Hash256(byte[] data)
    {
        if (data == null)
            throw new ArgumentNullException(nameof(data));
        using (SHA256 shA256 = (SHA256) new SHA256Managed())
            return shA256.ComputeHash(data);
    }

    private static string GetString(this SecureString password)
    {
        lock (Secure.SecurityLock)
        {
            IntPtr num = IntPtr.Zero;
            try
            {
                num = Marshal.SecureStringToGlobalAllocUnicode(password);
                return Marshal.PtrToStringUni(num);
            }
            finally
            {
                if (num != IntPtr.Zero)
                    Marshal.ZeroFreeGlobalAllocUnicode(num);
            }
        }
    }
    
    public static string ToHexString(this byte[] data)
    {
        int num = data != null ? data.Length : throw new ArgumentNullException(nameof (data));
        StringBuilder stringBuilder = new StringBuilder(num * 2);
        for (int index = 0; index < num; ++index)
            stringBuilder.AppendFormat("{0:X2}", (object) data[index]);
        return stringBuilder.ToString();
    }
}