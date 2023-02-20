// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEditor.PackageManager.Requests;

namespace Firebase
{
    internal static class PackageCheck
    {
        internal const string OldPackageScope = "com.rest.firebase";
        internal const string NewPackageScope = "com.rest.firebase";

        private static bool? canUpgrade;

        private static ListRequest listRequest;

        private static readonly List<string> oldPackages = new List<string>();

        [InitializeOnLoadMethod]
        public static void InitPackageCheck()
        {
            listRequest = Client.List();
            EditorApplication.update += Update;
        }

        private static void Update()
        {
            oldPackages.Clear();

            if (listRequest is { IsCompleted: true })
            {
                var packages = listRequest.Result.ToArray();

                foreach (var package in packages)
                {
                    if (package.packageId.Contains(OldPackageScope) &&
                        package.source == PackageSource.Registry &&
                        package.author.name == "Stephen Hodgson")
                    {
                        if (!canUpgrade.HasValue && EditorUtility.DisplayDialog("Attention!",
                                "This Firebase package scope has changed!\n\n" +
                                "com.rest.firebase.* -> com.rest.firebase.*\n\n" +
                                "Would you like to automatically update your package to the new scope?\n\n" +
                                "This will not change the version you're currently on.",
                                "Ok", "Later"))
                        {
                            canUpgrade = true;
                        }

                        oldPackages.Add(package.packageId);
                    }
                }

                if (canUpgrade.HasValue &&
                    canUpgrade.Value)
                {
                    UpgradePackage();
                }

                listRequest = null;
                EditorApplication.update -= Update;
            }
        }

        private static void UpgradePackage()
        {
            var newPackages = oldPackages.Select(oldPackage => oldPackage.Replace(OldPackageScope, NewPackageScope)).ToList();
            Client.AddAndRemove(newPackages.ToArray(), oldPackages.ToArray());
        }
    }
}
