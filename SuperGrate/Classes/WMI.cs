﻿using System;
using System.Management;
using System.Threading.Tasks;

namespace SuperGrate
{
    public static class WMI
    {
        /// <summary>
        /// Runs a WMI query on a remote host.
        /// </summary>
        /// <param name="Query">Query to run.</param>
        /// <param name="Host">Host to run the query on.</param>
        /// <returns>A task with results.</returns>
        public static Task<ManagementObjectCollection> Query(string Query, string Host)
        {
            return Task.Run(() =>
            {
                try
                {
                    ManagementScope scope = new ManagementScope(GetBestManagementScope(Host));
                    scope.Connect();
                    ObjectQuery query = new ObjectQuery(Query);
                    ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query);
                    ManagementObjectCollection collection = searcher.Get();
                    searcher.Dispose();
                    return collection;
                }
                catch (Exception e)
                {
                    Logger.Exception(e, "Failed to query via WMI.");
                    return null;
                }
            });
        }
        /// <summary>
        /// Gets the instance of a management object from a remote host.
        /// </summary>
        /// <param name="Host">Host to get object from.</param>
        /// <param name="InstancePath">Instance path to use.</param>
        /// <returns>A task and management object.</returns>
        public static Task<ManagementObject> GetInstance(string Host, string InstancePath)
        {
            return Task.Run(() =>
            {
                try
                {
                    ManagementObject mo = new ManagementObject(GetBestManagementScope(Host), InstancePath, new ObjectGetOptions(null, TimeSpan.FromSeconds(30), true));
                    mo.Get();
                    return mo;
                }
                catch (Exception e)
                {
                    Logger.Exception(e, "Failed to retrieve WMI object.");
                    return null;
                }
            });
        }
        /// <summary>
        /// Returns the best path to "ManagementScope" based on the host provided. (If the host provided is the current host then the best path is "\root\cimv2" otherwise it is "\\HOST\root\cimv2".)
        /// </summary>
        /// <param name="Host">Host to check against.</param>
        /// <returns>Either "\root\cimv2" or "\\HOST\root\cimv2".</returns>
        public static string GetBestManagementScope(string Host)
        {
            if (Misc.IsHostThisMachine(Host))
            {
                return @"\root\cimv2";
            }
            else
            {
                return @"\\" + Host + @"\root\cimv2";
            }
        }
    }
}
