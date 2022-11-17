
/***********************************************************************************************************
 * Produced by App Advisory - http://app-advisory.com
 * Facebook: https://facebook.com/appadvisory
 * Contact us: https://appadvisory.zendesk.com/hc/en-us/requests/new
 * App Advisory Unity Asset Store catalog: http://u3d.as/9cs
 * Developed by Gilbert Anthony Barouch - https://www.linkedin.com/in/ganbarouch
 ***********************************************************************************************************/




using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;


namespace AppAdvisory
{
	public class ReflectionUtils : MonoBehaviour 
	{
		public static System.Type FindClass(string className, string nameSpace = null, string assemblyName = null)
		{
			Assembly[] assemblies = System.AppDomain.CurrentDomain.GetAssemblies();

			foreach (Assembly asm in assemblies)
			{
				// The assembly must match the given one if any.
				if (!string.IsNullOrEmpty(assemblyName) && !asm.GetName().Name.Equals(assemblyName))
				{
					continue;
				}

				System.Type[] types = asm.GetTypes();
				foreach (System.Type t in types)
				{
					// The namespace must match the given one if any. Note that the type may not have a namespace at all.
					// Must be a class and of course class name must match the given one.
					if (t.IsClass && t.Name.Equals(className) && (string.IsNullOrEmpty(nameSpace) || nameSpace.Equals(t.Namespace)))
					{
						return t;
					}
				}
			}

			return null;
		}

		public static bool NamespaceExists(string nameSpace, string assemblyName = null)
		{
			Assembly[] assemblies = System.AppDomain.CurrentDomain.GetAssemblies();

			foreach (Assembly asm in assemblies)
			{
				// The assembly must match the given one if any.
				if (!string.IsNullOrEmpty(assemblyName) && !asm.GetName().Name.Equals(assemblyName))
				{
					continue;
				}

				System.Type[] types = asm.GetTypes();
				foreach (System.Type t in types)
				{
					// The namespace must match the given one if any. Note that the type may not have a namespace at all.
					// Must be a class and of course class name must match the given one.
					if (!string.IsNullOrEmpty(t.Namespace) && t.Namespace.Equals(nameSpace))
					{
						return true;
					}
				}
			}

			return false;
		}
	}
}