namespace Demo.Net.Blazor.Shared
{
    public abstract class MathWorkspaceBase : WorkspaceBase
    {
        private double _coefficienta;
        public virtual double CoefficientA
        {
            get => _coefficienta;
            set
            {
                _coefficienta = value;
            }
        }

        private double _coefficientb;
        public virtual double CoefficientB
        {
            get => _coefficientb;
            set
            {
                _coefficientb = value;
            }
        }

        private double _coefficientc;
        public virtual double CoefficientC
        {
            get => _coefficientc;
            set
            {
                _coefficientc = value;
            }
        }

    }
}
