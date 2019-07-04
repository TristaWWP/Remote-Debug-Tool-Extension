using EnvDTE;
using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.Collections.Generic;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalClient
{
    //class Buildpro
    //{
    //    //字段
    //    public Project BuildStart()
    //    {
    //        var StartBuid = (IVsSolutionBuildManager2)Package.GetServiceAsync(typeof(SVsSolutionBuildManager)).Result;
    //        IVsHierarchy startupProject;
    //        StartBuid.get_StartupProject(out startupProject);
    //        if (startupProject == null)
    //        {
    //           // MessageBox.Show("未检测到启动项，请确保是否已打开解决方案");
    //            return null;
    //        }
    //        else
    //        {
    //            StartBuid.StartUpdateProjectConfigurations(1, new[] { startupProject }, (uint)VSSOLNBUILDUPDATEFLAGS.SBF_OPERATION_BUILD, 0);//编译
    //            startupProject.GetProperty((uint)VSConstants.VSITEMID.Root, (int)__VSHPROPID.VSHPROPID_ExtObject, out var obj);
    //            var project = (obj as Project);
    //            return project;
    //        }
    //    }
    //}
}
