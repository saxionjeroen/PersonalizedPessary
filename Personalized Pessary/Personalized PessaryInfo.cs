using System;
using System.Drawing;
using Grasshopper.Kernel;

namespace Personalized_Pessary
{
    public class Personalized_PessaryInfo : GH_AssemblyInfo  {

    public override string Name
    {
        get
        {
            return "PersonalizedPessary";
        }
    }
    public override Bitmap Icon
    {
        get
        {
            //Return a 24x24 pixel bitmap to represent this GHA library.
            return null;
        }
    }
    public override string Description
    {
        get
        {
            //Return a short string describing the purpose of this GHA library.
            return "";
        }
    }
    public override Guid Id
    {
        get
        {
            return new Guid("9c0c298a-fc32-42cd-9370-790992e850aa");
        }
    }

    public override string AuthorName
    {
        get
        {
            //Return a string identifying you or your company.
            return "";
        }
    }
    public override string AuthorContact
    {
        get
        {
            //Return a string representing your preferred contact details.
            return "";
        }
    }
}
}
