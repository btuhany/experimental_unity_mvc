using System.IO;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEditor.Compilation;
using UnityEngine;

namespace Batuhan.MVC.Editor
{
    public class AssetGeneratorHelperEditor
    {
        private enum TemplateType
        {
            NewController,
            NewModel,
            NewView,
        }
        [MenuItem("Assets/Create/Batuhan/MVC/New Controller Script", false, 1)]
        private static void CreateNewController()
        {
            CreateAndOpenScriptFromTemplate(TemplateType.NewController, "NewController.cs");
        }

        [MenuItem("Assets/Create/Batuhan/MVC/New Model Script", false, 1)]
        private static void CreateNewModel()
        {
            CreateAndOpenScriptFromTemplate(TemplateType.NewModel, "NewModel.cs");
        }
        [MenuItem("Assets/Create/Batuhan/MVC/New View Script", false, 1)]
        private static void CreateNewView()
        {
            CreateAndOpenScriptFromTemplate(TemplateType.NewView, "NewView.cs");
        }
        [MenuItem("Assets/Create/Batuhan/MVC//New MVC Entity", false, 2)]
        private static void CreateBaseMVCEntity()
        {
            CreateAndOpenScriptFromTemplate(TemplateType.NewModel, "NewModel.cs");
            CreateAndOpenScriptFromTemplate(TemplateType.NewView, "NewView.cs");
            CreateAndOpenScriptFromTemplate(TemplateType.NewController, "NewController.cs");
        }

        private static void CreateAndOpenScriptFromTemplate(TemplateType templateType, string fileName)
        {
            string selectedPath = GetSelectedPath();
            if (string.IsNullOrEmpty(selectedPath))
            {
                Debug.LogError("Invalid folder selection.");
                return;
            }

            string fullPath = Path.Combine(selectedPath, fileName);
            if (File.Exists(fullPath))
            {
                Debug.LogWarning("File already exists: " + fileName);
                return;
            }

            string defaultNamespace = GetDefaultNamespace(selectedPath);
            string templateContent = GetTemplateContent(defaultNamespace, templateType);
            
            File.WriteAllText(fullPath, templateContent);
            AssetDatabase.Refresh();

            MonoScript script = AssetDatabase.LoadAssetAtPath<MonoScript>(fullPath);
            if (script != null)
            {
                AssetDatabase.OpenAsset(script);
            }
        }

        private static string GetSelectedPath()
        {
            string path = AssetDatabase.GetAssetPath(Selection.activeObject);
            if (string.IsNullOrEmpty(path)) return "Assets";
            if (Path.HasExtension(path)) path = Path.GetDirectoryName(path);
            return path;
        }
        private static string GetDefaultNamespace(string filePath)
        {
            string assemblyName = CompilationPipeline.GetAssemblies()
                .FirstOrDefault(a => a.sourceFiles.Any(f => f.Contains(filePath)))?.name;

            if (!string.IsNullOrEmpty(assemblyName))
            {
                string rootNamespace = CompilationPipeline.GetAssemblies()
                    .FirstOrDefault(a => a.name == assemblyName)?.rootNamespace;

                return !string.IsNullOrEmpty(rootNamespace) ? rootNamespace : assemblyName;
            }

            return "DefaultNamespace";
        }
        private static string GetTemplateContent(string defaultNamespace, TemplateType templateType)
        {
            switch (templateType)
            {
                case TemplateType.NewController:
                    return @$"using Batuhan.MVC.Core;
namespace {defaultNamespace}.Entities.NewEntity 
{{ 
    public class NewController : IController 
    {{ 
    }} 
}}";
                case TemplateType.NewModel:
                    return @$"using Batuhan.MVC.Core;
namespace {defaultNamespace}.Entities.NewEntity 
{{ 
    public class NewModel : IModel
    {{ 
    }} 
}}";
                case TemplateType.NewView:
                    return @$"using Batuhan.MVC.Core;
namespace {defaultNamespace}.Entities.NewEntity 
{{ 
    public class NewView : IView
    {{ 
    }} 
}}";
                default:
                    return string.Empty;
            }
        }
    }
}
