using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Observer
{
    class Program
    {
        public abstract class Observer // شیءایی که قرار است وضعیت را مشاهده یا رصد کند
        {
            protected Observable Observable;
            public abstract void Update();
        }
        public class Observable //شیءایی که قرار است وضعیت آن رصد شود
        {
            private readonly List<Observer> _observers = new List<Observer>();
            public void Attach(Observer observer)
            {
                _observers.Add(observer);
            }
            public void Dettach(Observer observer)
            {
                _observers.Remove(observer);
            }

            public void NotifyAllObservers()             // با مرور لیست رصدکنندگانش، رویداد آپ دیت آن‌ها را صدا میزند تا تغییر وضعیت کلید به آن‌ها گزارش داده شود
            {
                foreach (var observer in _observers)
                {
                    observer.Update();
                }
            }
        }
        public class Switch : Observable
        {
            private bool _state;

            public bool ChangeState
            {
                set
                {
                    _state = value;
                    NotifyAllObservers();
                }
                get { return _state; }
            }
        }
        public class RedLED : Observer
        {
            private bool _on = false;
            public override void Update()
            {
                _on = !_on;
                Console.WriteLine($"Red LED is {((_on) ? "On" : "Off")}");
            }
        }
        public class BlueLED : Observer
        {
            private bool _on = false;
            public override void Update()
            {
                _on = !_on;
                Console.WriteLine($"Blue LED is {((_on) ? "On" : "Off")}");
            }
        }
        public class GreenLED : Observer
        {
            private bool _on = false;
            public override void Update()
            {
                _on = !_on;
                Console.WriteLine($"Green LED is {((_on) ? "On" : "Off")}");
            }
        }

        static void Main(string[] args)
        {
            var greenLed = new GreenLED();
            var redLed = new RedLED();
            var blueLed = new BlueLED();

            var switchKey = new Switch();
            switchKey.Attach(greenLed);
            switchKey.Attach(redLed);
            switchKey.Attach(blueLed);

            switchKey.ChangeState = true;
            switchKey.ChangeState = false;
            Console.ReadKey();
        }
    }
}
