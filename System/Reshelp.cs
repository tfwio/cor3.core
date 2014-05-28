/*
 * Created by SharpDevelop.
 * User: oio
 * Date: 5/28/2011
 * Time: 8:49 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.ComponentModel;
using System.Globalization;
using System.Resources;

namespace System.Cor3
{
	public class ResourceUtil
	{
		private string baseName;
		private Type   baseType;

		public string GetString(string key) { return ResourceManager.GetString(key); }
		public string GetString(string key, object value) { return string.Format(ResourceManager.GetString(key),value); }
		
		private ResourceManager resourceMan;
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal ResourceManager ResourceManager
		{
			get
			{
				if (object.ReferenceEquals(this.resourceMan, null))
				{
					ResourceManager resourceManager = new ResourceManager(baseName, baseType.Assembly);
					this.resourceMan = resourceManager;
				}
				return this.resourceMan;
			}
		}

		private CultureInfo resourceCulture;
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal CultureInfo Culture { get { return this.resourceCulture; } set { this.resourceCulture = value; } }
		
		public ResourceUtil(string baseNs, Type asmType)
		{
			baseName = baseNs;
			baseType = asmType;
		}
	}
}
