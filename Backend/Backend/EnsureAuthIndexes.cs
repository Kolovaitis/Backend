using AspNet.Identity.MongoDB;

namespace Backend
{
    public class EnsureAuthIndexes
    {
        public static void Exist()
        {
            var context = ApplicationIdentityContext.Create();
            IndexChecks.EnsureUniqueIndexOnEmail(context.Users);
        }
    }
}
