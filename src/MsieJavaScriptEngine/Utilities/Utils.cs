﻿using System;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

using MsieJavaScriptEngine.Resources;

namespace MsieJavaScriptEngine.Utilities
{
	internal static class Utils
	{
		/// <summary>
		/// Determines whether the current process is a 64-bit process
		/// </summary>
		/// <returns>true if the process is 64-bit; otherwise, false</returns>
		[MethodImpl((MethodImplOptions)256 /* AggressiveInlining */)]
		public static bool Is64BitProcess()
		{
#if NETSTANDARD1_3
			bool is64Bit = IntPtr.Size == 8;
#else
			bool is64Bit = Environment.Is64BitProcess;
#endif

			return is64Bit;
		}

		/// <summary>
		/// Gets a content of the embedded resource as string
		/// </summary>
		/// <param name="resourceName">Resource name</param>
		/// <param name="type">Type from assembly that containing an embedded resource</param>
		/// <returns>Сontent of the embedded resource as string</returns>
		public static string GetResourceAsString(string resourceName, Type type)
		{
			Assembly assembly = type.GetTypeInfo().Assembly;

			return GetResourceAsString(resourceName, assembly);
		}

		/// <summary>
		/// Gets a content of the embedded resource as string
		/// </summary>
		/// <param name="resourceName">Resource name</param>
		/// <param name="assembly">Assembly that containing an embedded resource</param>
		/// <returns>Сontent of the embedded resource as string</returns>
		public static string GetResourceAsString(string resourceName, Assembly assembly)
		{
			using (Stream stream = assembly.GetManifestResourceStream(resourceName))
			{
				if (stream == null)
				{
					throw new NullReferenceException(
						string.Format(CommonStrings.Resources_ResourceIsNull, resourceName));
				}

				using (var reader = new StreamReader(stream))
				{
					return reader.ReadToEnd();
				}
			}
		}

		/// <summary>
		/// Gets a text content of the specified file
		/// </summary>
		/// <param name="path">File path</param>
		/// <param name="encoding">Content encoding</param>
		/// <returns>Text content</returns>
		public static string GetFileTextContent(string path, Encoding encoding = null)
		{
			if (!File.Exists(path))
			{
				throw new FileNotFoundException(
					string.Format(CommonStrings.Common_FileNotExist, path), path);
			}

			string content;

			using (var stream = File.OpenRead(path))
			using (var reader = new StreamReader(stream, encoding ?? Encoding.UTF8))
			{
				content = reader.ReadToEnd();
			}

			return content;
		}
	}
}