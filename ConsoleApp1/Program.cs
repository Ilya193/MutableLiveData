interface ILiveData<T>
{
    void Observe(callback<T> func); 

}

delegate void callback<T>(T data);

class MutableLiveData<T> : ILiveData<T>
{

    private T data;

    public T value
    {
        set
        {
            data = value;
            foreach (callback<T> func in callbacks)
            {
                func.Invoke(value);
            }
        }
    }

    private List<callback<T>> callbacks = new List<callback<T>>();


    public MutableLiveData(T data) { this.value = data; this.data = data; }

    public void Observe(callback<T> func)
    {
        callbacks.Add(func);
    }
}

namespace Application
{
    class Program
    {
        static void Main(string[] args)
        {
            MutableLiveData<string> livedata = new MutableLiveData<string>("");

            livedata.Observe(delegate (string data)
            {
                Console.WriteLine(data);
            });

            Thread.Sleep(2000);
            livedata.value = "NEW DATA";
        }
    }
}

