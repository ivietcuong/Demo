namespace Demo.Net.Blazor.Shared
{
    public abstract class MathWorkspaceBase : WorkspaceBase
    {
        private bool _disabled = false;
        private string? _validationMessage;

        public bool Disabled
        {
            get { return _disabled; }
            set { _disabled = value; }
        }

        public string? ValidationMessage 
        {
            get => _validationMessage;
            set => _validationMessage = value;
        }

        public virtual double CoefficientA 
        {
            get; set; 
        }

        public virtual double CoefficientB 
        {
            get; set; 
        }

        public virtual double CoefficientC 
        {
            get; set; 
        }
       
        public virtual string Validation()
        {
            return string.Empty;
        }
    }
}
