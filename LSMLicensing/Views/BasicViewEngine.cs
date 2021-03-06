﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LicenseRegistrationAPI.Views
{
    public class BasicView : IView
    {
        public BasicView(string path)
        {
            Path = path;
        }
        public string Path { get; private set; }

        public Task RenderAsync(ViewContext context)
        {
            var template = File.ReadAllText(Path);

            var processedOutput = template.Replace("{{Message}}", context.ViewData["Message"]?.ToString());

            return context.Writer.WriteAsync(processedOutput);
        }
    }
    public class BasicViewEngine : IViewEngine
    {
        private const string ViewExtension = ".html";
        private string[] _viewLocationFormats = {
      "Views/{1}/{0}" + ViewExtension,
      "Views/Shared/{0}" + ViewExtension
    };
        public ViewEngineResult FindView(ActionContext context, string viewName, bool isMainPage)
        {
            if (context.ActionDescriptor.RouteValues.TryGetValue("controller", out var controllerName))
            {
                var checkedLocations = new List<string>();

                foreach (var locationFormat in _viewLocationFormats)
                {
                    var possibleViewLocation = string.Format(locationFormat, viewName, controllerName);

                    if (File.Exists(possibleViewLocation))
                    {
                        return ViewEngineResult.Found(viewName, new BasicView(possibleViewLocation));
                    }
                    checkedLocations.Add(possibleViewLocation);
                }

                return ViewEngineResult.NotFound(viewName, checkedLocations);
            }
            throw new Exception("Controller route value not found.");
        }

        public ViewEngineResult GetView(string executingFilePath, string viewPath, bool isMainPage)
        {
            if (string.IsNullOrEmpty(viewPath) || !viewPath.EndsWith(ViewExtension, StringComparison.OrdinalIgnoreCase))
            {
                return ViewEngineResult.NotFound(viewPath, Enumerable.Empty<string>());
            }

            var appRelativePath = GetAbsolutePath(executingFilePath, viewPath);

            if (File.Exists(appRelativePath))
            {
                return ViewEngineResult.Found(viewPath, new BasicView(appRelativePath));
            }

            return ViewEngineResult.NotFound(viewPath, new List<string> { appRelativePath });
        }

        private static string GetAbsolutePath(string executingFilePath, string viewPath)
        {
            if (IsAbsolutePath(viewPath))
            {
                // An absolute path already; no change required.
                return viewPath.Replace("~/", string.Empty);
            }

            if (string.IsNullOrEmpty(executingFilePath))
            {
                return $"/{viewPath}";
            }

            var index = executingFilePath.LastIndexOf('/');
            return executingFilePath.Substring(0, index + 1) + viewPath;
        }

        private static bool IsAbsolutePath(string name) => name.StartsWith("~/") || name.StartsWith("/");
    }
}
