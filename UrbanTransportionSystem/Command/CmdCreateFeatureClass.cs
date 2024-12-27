using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.IO;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using System.Windows.Forms;
using ESRI.ArcGIS.DataSourcesGDB;
using System.Collections.Generic;
using structSet;


namespace UrbanTransportionSystem
{
    /// <summary>
    /// Command that works in ArcMap/Map/PageLayout
    /// </summary>
    [Guid("94b8878f-f7ee-4cb4-9c0c-ebc3e389124b")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("UrbanTransportionSystem.CmdCreateFeatureClass")]
    public sealed class CmdCreateFeatureClass : BaseCommand
    {
        private IHookHelper m_hookHelper = null;
        private string pathSave = null;
        private string featureClassName = null;
        private esriGeometryType featureType;
        private List<fieldInfo> additionalFields = null;


        #region COM Registration Function(s)
        [ComRegisterFunction()]
        [ComVisible(false)]

        static void RegisterFunction(Type registerType)
        {
            // Required for ArcGIS Component Category Registrar support
            ArcGISCategoryRegistration(registerType);

            //
            // TODO: Add any COM registration code here
            //
        }

        [ComUnregisterFunction()]
        [ComVisible(false)]
        static void UnregisterFunction(Type registerType)
        {
            // Required for ArcGIS Component Category Registrar support
            ArcGISCategoryUnregistration(registerType);

            //
            // TODO: Add any COM unregistration code here
            //
        }

        #region ArcGIS Component Category Registrar generated code
        /// <summary>
        /// Required method for ArcGIS Component Category registration -
        /// Do not modify the contents of this method with the code editor.
        /// </summary>
        private static void ArcGISCategoryRegistration(Type registerType)
        {
            string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
            MxCommands.Register(regKey);
            ControlsCommands.Register(regKey);
        }
        /// <summary>
        /// Required method for ArcGIS Component Category unregistration -
        /// Do not modify the contents of this method with the code editor.
        /// </summary>
        private static void ArcGISCategoryUnregistration(Type registerType)
        {
            string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
            MxCommands.Unregister(regKey);
            ControlsCommands.Unregister(regKey);
        }

        #endregion
        #endregion
        public CmdCreateFeatureClass(string path, string name, esriGeometryType type, ref List<fieldInfo> Fields)
        {
            //
            // TODO: Define values for the public properties
            //
            base.m_category = ""; //localizable text
            base.m_caption = "";  //localizable text 
            base.m_message = "This should work in ArcMap/MapControl/PageLayoutControl";  //localizable text
            base.m_toolTip = "";  //localizable text
            base.m_name = "";   //unique id, non-localizable (e.g. "MyCategory_MyCommand")

            try
            {
                //
                // TODO: change bitmap name if necessary
                //
                string bitmapResourceName = GetType().Name + ".bmp";
                base.m_bitmap = new Bitmap(GetType(), bitmapResourceName);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message, "Invalid Bitmap");
            }

            pathSave = path;
            featureClassName = name;
            featureType = type;
            additionalFields = Fields;
        }

        #region Overridden Class Methods

        /// <summary>
        /// Occurs when this command is created
        /// </summary>
        /// <param name="hook">Instance of the application</param>
        public override void OnCreate(object hook)
        {
            if (hook == null)
                return;

            try
            {
                m_hookHelper = new HookHelperClass();
                m_hookHelper.Hook = hook;
                if (m_hookHelper.ActiveView == null)
                    m_hookHelper = null;
                FrmCreateFeatureClass frmCreateFeatureClass = new FrmCreateFeatureClass(m_hookHelper);
            }
            catch
            {
                m_hookHelper = null;
            }

            if (m_hookHelper == null)
                base.m_enabled = false;
            else
                base.m_enabled = true;
            // TODO:  Add other initialization code
        }

        /// <summary>
        /// Occurs when this command is clicked
        /// </summary>
        public override void OnClick()
        {
            try
            {
                pathSave = pathSave.Replace("/", "\\");
                string fileExtension = System.IO.Path.GetExtension(pathSave);
                if (!fileExtension.Equals(".gdb", StringComparison.OrdinalIgnoreCase))
                {
                    IWorkspaceFactory workspaceFactory = new ShapefileWorkspaceFactoryClass();
                    IWorkspace ipWorkspace = workspaceFactory.OpenFromFile(pathSave, 0);
                    if (ipWorkspace == null)
                    {
                        throw new ApplicationException("无法打开工作空间");
                    }

                    ISpatialReferenceFactory2 spatialReferenceFactory = new SpatialReferenceEnvironmentClass();
                    ISpatialReference ipSr = spatialReferenceFactory.CreateGeographicCoordinateSystem((int)esriSRGeoCSType.esriSRGeoCS_WGS1984);
                    if (ipSr == null)
                    {
                        throw new ApplicationException("无法创建空间参考");
                    }

                    IFeatureClass featureClass = CreateFeatureClass((IFeatureWorkspace)ipWorkspace, null, featureClassName, ipSr, featureType);
                    AddField(featureClass);
                }
                else
                {
                    IWorkspaceFactory workspaceFactory = new FileGDBWorkspaceFactoryClass();

                    IWorkspace ipWorkspace = workspaceFactory.OpenFromFile(pathSave, 0);
                    if (ipWorkspace == null)
                    {
                        throw new ApplicationException("无法打开工作空间");
                    }

                    ISpatialReferenceFactory2 spatialReferenceFactory = new SpatialReferenceEnvironmentClass();
                    ISpatialReference ipSr = spatialReferenceFactory.CreateGeographicCoordinateSystem((int)esriSRGeoCSType.esriSRGeoCS_WGS1984);
                    if (ipSr == null)
                    {
                        throw new ApplicationException("无法创建空间参考");
                    }

                    IFeatureClass featureClass = CreateFeatureClass((IFeatureWorkspace)ipWorkspace, null, featureClassName, ipSr, featureType);
                    AddField(featureClass);
                }
                MessageBox.Show("创建成功");
            }
            catch (Exception ex)
            {
                MessageBox.Show("创建失败" + ex);
            }
        }

        #endregion

        private IFeatureClass CreateFeatureClass(IFeatureWorkspace ipWorkspace, string dsName, string fcName, ISpatialReference ipSr, esriGeometryType type)
        {
            // 设置字段组
            IFieldsEdit ipFields = (IFieldsEdit)new Fields();
            ipFields.FieldCount_2 = 2;

            // 设置字段数
            IFieldEdit ipField = (IFieldEdit)new Field();
            ipField.Name_2 = "ObjectID";
            ipField.AliasName_2 = "FID";
            ipField.Type_2 = esriFieldType.esriFieldTypeOID;
            ipFields.set_Field(0, ipField);


            IGeometryDefEdit ipGeoDef = (IGeometryDefEdit)new GeometryDef();
            ipGeoDef.SpatialReference_2 = ipSr;

            ipGeoDef.GeometryType_2 = type;
            IFieldEdit ipField3 = (IFieldEdit)new Field();
            ipField3.Name_2 = "Shape";
            ipField3.AliasName_2 = "shape";
            ipField3.Type_2 = esriFieldType.esriFieldTypeGeometry;
            ipField3.GeometryDef_2 = ipGeoDef;
            ipFields.set_Field(1, ipField3);
            IFeatureClass ipFeatCls = null;
            ipFeatCls = ipWorkspace.CreateFeatureClass(fcName, ipFields, null, null, esriFeatureType.esriFTSimple, "Shape", "");
            return ipFeatCls;
        }
        private void AddField(IFeatureClass featureClass)
        {
            if (additionalFields != null)
            {
                foreach (fieldInfo fieldInfo in additionalFields)
                {
                    IFieldEdit customField = (IFieldEdit)new Field();
                    customField.Name_2 = fieldInfo.FieldName;
                    customField.AliasName_2 = fieldInfo.FieldName;
                    switch (fieldInfo.FieldType)
                    {
                        case "Int":
                            customField.Type_2 = esriFieldType.esriFieldTypeInteger;
                            break;
                        case "Text":
                            customField.Type_2 = esriFieldType.esriFieldTypeString;
                            break;
                        case "Double":
                            customField.Type_2 = esriFieldType.esriFieldTypeDouble;
                            break;
                        case "date":
                            customField.Type_2 = esriFieldType.esriFieldTypeDate;
                            break;
                        default:
                            MessageBox.Show($"不识别的字段类型: {fieldInfo.FieldType}");
                            continue; 
                    }                 
                    featureClass.AddField(customField);
                }
            }
        }


    }
}




