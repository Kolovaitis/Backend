using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IAmIt.Service.BoardService;
using IAmIt.Service.CardService;
using IAmIt.Service.ColumnService;
using IAmIt.Service.ProjectService;
using IAmIt.Service.TeamService;

namespace IAmIt.Service
{
    public class ServiceModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IProjectService>().To<ProjectService.ProjectService>();
            Bind<IBoardService>().To<BoardService.BoardService>();
            Bind<IColumnService>().To<ColumnService.ColumnService>();
            Bind<ICardService>().To<CardService.CardService>();
            Bind<ITeamService>().To<TeamService.TeamService>();
        }
    }
}
