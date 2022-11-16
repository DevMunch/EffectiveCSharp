using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EffectiveCSharp.Effective01
{
    internal class Ex001
    {
        // 지역변수를 선언할 때는 var를 사용하는 것이 낫다.
        public void Test()
        {
            string anotherParameter = null;
            // 아래의 경우 메서드의 이름만으로는 반환 타입을 알 수 없다.
            var result = someObject.DoSomeWork(anotherParameter);

            // 위의 같은 경우에도 아래와 같이 변수명을 달리하면 의미를 명확하게 드러낼 수 있다.
            // 변수 이름을 통해 Product 타입임을 미루어 짐작할 수 있다.
            var HighestSellingProduct = someObject.DoSomeWork(anotherParameter);

            // 그러나 이것들은 모두 짐작일 뿐이고 추론한 타입이 달라서 문제가 될 수 있고, 컴파일러가
            // 컴파일시 개발자의 생각을 알 수없어 다른 결과를 초래할 수 있다.
            // 하지만 이러한 이유로 var를 사용하는 것이 적절하지 않다고 말하는 것은 가혹하다.

            
        }

        // 때로는 변수의 타입을 명시적으로 선언하는 것보다 컴파일러에게 타입을 추론하도록 맡기는 편이 더 낫다.
        // 아래의 코드는 데이터베이스에서 특정 문자열로 시작하는 고객의 이름을 검색하는 코드이다.
        // 아래의 코드는 var로 해야 성능에 매우 유리해진다.
        // IEnumerable<string>로 개발자가 지정해 그것을 상속받는 IQueryable<string>의 모든 장점을 잃게 된다.
        // 결국 컴파일러는 IEnumerable<string>이 타당하다고 보고 Queryalbe.Where가 아닌 Enumerable.Where로 해석된다.
        // 결국 이 두번째 부분에서 성능에 큰 문제가 생긴다.
        public IEnumerable<string> FindCustomerStartingWith1(string start)
        {
            IEnumerable<string> q = from c in db.Customers select c.ContectName;

            var q2 = q.Where(s=>s.StartsWith(start));
            return q2;
        }

        // 수정된 버전
        // var로 선언해 이제 IQueryable<string>로 추론되어 성능이 개선된다.
        public IEnumerable<string> FindCustomerStartingWith(string start)
        {
            var q = from c in db.Customers select c.ContectName;

            var q2 = q.Where(s => s.StartsWith(start));
            return q2;
        }

        // 요약.
        // 코드를 읽을 때 타입을 명시적으로 드러내야 하는 경우가 아니라면 var를 사용하는 것이 좋다.
        // 내장 타입(int, float, double...)을 선언할 때는 명시적으로 타입을 선언하는 편이 낫다.
        // 그 외에는 모두 var를 사용해보자.
    }

    class db { public static List<Customer> Customers = new List<Customer>(); }
    class Customer { public string ContectName; }
    class someObject
    {
        public static string DoSomeWork(string job)
        {
            return "apple";
        }
    }
}