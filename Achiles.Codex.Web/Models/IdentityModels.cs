using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Raven.Client;

namespace Achiles.Codex.Web.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        private List<UserLoginInfo> _providers = new List<UserLoginInfo>();

        public List<UserLoginInfo> Providers
        {
            get { return _providers; }
            set { _providers = value; }
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }
    }

    public class RavenUserStore : IUserStore<ApplicationUser>, IUserLoginStore<ApplicationUser>, IUserRoleStore<ApplicationUser>
    {
        private readonly IDocumentSession _session;

        public RavenUserStore(IDocumentSession session)
        {
            _session = session;
        }

        public Task CreateAsync(ApplicationUser user)
        {
            return Task.Factory.StartNew(x =>
            {
                _session.Store(user);
                _session.SaveChanges();
            },null);
        }

        public Task UpdateAsync(ApplicationUser user)
        {
            return Task.Factory.StartNew(x =>
            {
                _session.Store(user);
                _session.SaveChanges();
            }, null);
        }

        public Task DeleteAsync(ApplicationUser user)
        {
            throw new NotSupportedException();
        }

        private ApplicationUser FindById(string userId)
        {
            var usr = _session.Load<ApplicationUser>(userId);
            return usr;
        }

        public Task<ApplicationUser> FindByIdAsync(string userId)
        {
            return Task.Run(()=>_session.Load<ApplicationUser>(userId));
        }

        public Task<ApplicationUser> FindByNameAsync(string userName)
        {
            return Task.Factory.StartNew(x => _session.Query<ApplicationUser>()
                .FirstOrDefault(u => u.UserName == userName), null);
        }

        public void Dispose()
        {
            
        }

        public Task AddLoginAsync(ApplicationUser user, UserLoginInfo login)
        {
            return Task.Factory.StartNew(x =>
            {
                user.Providers.Add(login);
                _session.Store(user);
                _session.SaveChanges();
            }, null);
        }

        public Task RemoveLoginAsync(ApplicationUser user, UserLoginInfo login)
        {
            return Task.Factory.StartNew(x =>
            {
                var entity = _session.Load<ApplicationUser>(user.Id);
                _session.Delete(entity);
                _session.SaveChanges();
            }, null);
        }

        public Task<IList<UserLoginInfo>> GetLoginsAsync(ApplicationUser user)
        {
            return Task.Factory.StartNew(x =>
            {
                var entity = _session.Load<ApplicationUser>(user.Id);
                if (entity != null)
                    return (IList<UserLoginInfo>)entity.Providers;
                
                    return null;
            }, null);
        }

        public Task<ApplicationUser> FindAsync(UserLoginInfo login)
        {
            return Task.Factory.StartNew(x => _session.Query<ApplicationUser>()
                .FirstOrDefault(u => u.Providers.Any(p => p.ProviderKey == login.ProviderKey)), null);
        }

        public Task AddToRoleAsync(ApplicationUser user, string role)
        {
            return Task.Factory.StartNew(() => user.Roles.Add(
                new IdentityUserRole()
                {
                    Role = new IdentityRole()
                    {
                        Name = role
                    },
                }));
        }

        public Task RemoveFromRoleAsync(ApplicationUser user, string role)
        {
            return Task.Factory.StartNew(() =>
            {
                var r = user.Roles.FirstOrDefault(x => x.Role.Name == role);
                if (r != null)
                    user.Roles.Remove(r);
            });
        }

        public Task<IList<string>> GetRolesAsync(ApplicationUser user)
        {
            return Task.Factory.StartNew(() => (IList<string>)user.Roles.Select(r => r.Role.Name).ToList());
        }

        public Task<bool> IsInRoleAsync(ApplicationUser user, string role)
        {
            return Task.Factory.StartNew(() => user.Roles.Any(r => r.Role.Name == role));
        }
    }
}