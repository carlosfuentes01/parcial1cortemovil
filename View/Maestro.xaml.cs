

namespace llamada;

public partial class Maestro : ContentPage
{
	public Maestro()
	{
		InitializeComponent();
		this.BindingContext = this;
	}

    private void Button_Clicked(object sender, EventArgs e)
    {
		App.Gotopage(new Crear());
    }
}