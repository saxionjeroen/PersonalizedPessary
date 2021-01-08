using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;

// In order to load the result of this wizard, you will also need to
// add the output bin/ folder of this project to the list of loaded
// folder in Grasshopper.
// You can use the _GrasshopperDeveloperSettings Rhino command for that.

namespace Personalized_Pessary
{
    
    public class PersonalizedPessaryComponent : GH_Component
    {
        private int models = 4;

        /// <summary>
        /// Each implementation of GH_Component must provide a public 
        /// constructor without any arguments.
        /// Category represents the Tab in which the component will appear, 
        /// Subcategory the panel. If you use non-existing tab or panel names, 
        /// new tabs/panels will automatically be created.
        /// </summary>
        public PersonalizedPessaryComponent()
          : base("Personalized Pessary", "Pessary",
              "A pessary that is personalized ",
              "Saxion Smart Materials", "Personalized Pessary")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddNumberParameter("Size", "S", "The size of a pessary", GH_ParamAccess.item);
            pManager.AddNumberParameter("Thickness", "T", "The thickness of a pessary", GH_ParamAccess.item);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddBrepParameter("Pessary", "Pes", "A customized pessary based on size and thickness", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object can be used to retrieve data from input parameters and 
        /// to store data in output parameters.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            double size = double.NaN;
            double thickness = double.NaN;

            if (!DA.GetData(0, ref size)) { return; }
            if (!DA.GetData(1, ref thickness)) { return; }

            if (!Rhino.RhinoMath.IsValidDouble(size)) { return; }
            if (!Rhino.RhinoMath.IsValidDouble(size)) { return; }

            if (size <= thickness)
            {
                AddRuntimeMessage(GH_RuntimeMessageLevel.Error, "The thickness of a pessary cannot be bigger than the size.");
                return;
            }
            Brep pessary = CreateBrep(size, thickness);

            DA.SetData(0, pessary);
        }

        private Brep CreateBrep(double size, double thickness)
        {
            Rhino.Geometry.Point3d center = new Rhino.Geometry.Point3d(0, 0, 0);
            Circle pessarySize = new Circle(center, size);
            Plane[] perpFrame = pessarySize.ToNurbsCurve().GetPerpendicularFrames(new List<double> { 0, 1, 2, 3 });
            Circle pessaryThickness = new Circle(perpFrame[0], thickness);

            Brep[] b = Rhino.Geometry.Brep.CreateFromSweep(pessarySize.ToNurbsCurve(), pessaryThickness.ToNurbsCurve(), true, 0.01);

            return b[0];
        }

        /// <summary>
        /// Provides an Icon for every component that will be visible in the User Interface.
        /// Icons need to be 24x24 pixels.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                // You can add image files to your project resources and access them like this:
                //return Resources.IconForThisComponent;
                return null;
            }
        }

        /// <summary>
        /// Each component must have a unique Guid to identify it. 
        /// It is vital this Guid doesn't change otherwise old ghx files 
        /// that use the old ID will partially fail during loading.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("4028e8ca-866b-4b5b-a040-b647fd1df0aa"); }
        }
    }
}
