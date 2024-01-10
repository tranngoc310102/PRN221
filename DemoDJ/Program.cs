// See https://aka.ms/new-console-template for more information
class DongCoChina : iDongCo
{
	public void Start()
	{
		Console.WriteLine("Dong co China đang hoat dong");
	}
}

class DongCoAmerican : iDongCo
{
	public void Start()
	{
		Console.WriteLine("Dong co American đang hoat dong");
	}
}

interface iDongCo
{
	public void Start();
}
class Car
{
	void Move(iDongCo dongCo)
	{
		dongCo.Start();
		Console.WriteLine("Car Move");
	}

	public static void Main()
	{
		iDongCo dc = new DongCoAmerican();
		Car c1 = new Car();
		c1.Move(dc);
	}
}

