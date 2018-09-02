using System;
using Realms;
using System.Linq;
using System.Diagnostics;


namespace RealmSample
{
    public class RealmTest
    {
        /// <summary>
        /// Realm をインスタンス化する
        /// </summary>
        public void ExcecuteSample01()
        {
            //スコープ外になった際にデータベースは自動でクローズされる。。
            var realm = Realm.GetInstance();
        }

        /// <summary>
        /// Realmにデータを追加する。
        /// </summary>
        public void ExcecuteSample02()
        {
            var realm = Realm.GetInstance();
           
            //Realmデータベースのパスを出力
            Debug.WriteLine(realm.Config.DatabasePath);

            realm.Write(() =>
            {
                realm.Add(new Person { Name = "山田太郎", Age = 23 });
            });
            
        }

        /// <summary>
        /// 追加したデータを検索する
        /// </summary>
        public void ExcecuteSample03()
        {
            var realm = Realm.GetInstance();

            //Realmデータベースのパスを出力
            Debug.WriteLine(realm.Config.DatabasePath);

            realm.Write(() =>
            {
                realm.Add(new Person { Name = "山田太郎", Age = 23 });
                realm.Add(new Person { Name = "佐藤花子", Age = 18 });
                realm.Add(new Person { Name = "田中哲朗", Age = 33 });
            });

            var people = realm.All<Person>().Where(p => p.Age > 20);

            foreach (var person in people)
            {
                Debug.WriteLine("Name=" + person.Name);
            }

        }

        /// <summary>
        /// 検索データを更新する
        /// </summary>
        public void ExcecuteSample04()
        {
            var realm = Realm.GetInstance();

            //Realmデータベースのパスを出力
            Debug.WriteLine(realm.Config.DatabasePath);

            realm.Write(() =>
            {
                realm.Add(new Person { Name = "山田太郎", Age = 23 });
                realm.Add(new Person { Name = "佐藤花子", Age = 18 });
                realm.Add(new Person { Name = "田中哲朗", Age = 33 });
            });

            //１件目の山田さんのみ抽出
            var person = realm.All<Person>().Where(p => p.Name.Contains("山田")).FirstOrDefault<Person>();

            //プロパティにセットするだけでデータ更新される
            realm.Write(() =>
            {
                person.Age = 30;
            });
        }

        /// <summary>
        /// 検索データを削除する
        /// </summary>
        public void ExcecuteSample05()
        {
            var realm = Realm.GetInstance();

            //Realmデータベースのパスを出力
            Debug.WriteLine(realm.Config.DatabasePath);

            realm.Write(() =>
            {
                realm.Add(new Person { Name = "山田太郎", Age = 23 });
                realm.Add(new Person { Name = "佐藤花子", Age = 18 });
                realm.Add(new Person { Name = "田中哲朗", Age = 33 });
            });

            //１件目の山田さんのみ抽出
            var person = realm.All<Person>().Where(p => p.Name.Contains("山田")).FirstOrDefault<Person>();
            
            realm.Write(() =>
            {
                realm.Remove(person);
            });
        }

        /// <summary>
        /// トランザクション制御
        /// </summary>
        public void ExcecuteSample06()
        {
            var realm = Realm.GetInstance();

            //Realmデータベースのパスを出力
            Debug.WriteLine(realm.Config.DatabasePath);

            using (var trans = realm.BeginWrite())
            {
                try 
                {
                    realm.Add(new Person { Name = "山田太郎", Age = 23 });
                    realm.Add(new Person { Name = "佐藤花子", Age = 18 });
                    realm.Add(new Person { Name = "田中哲朗", Age = 33 });
                    //throw new Exception("Fail!");
                    trans.Commit();

                } catch (Exception ex){
                    Debug.WriteLine("データ更新に失敗しました。" + ex.Message);
                    trans.Rollback();
                }

            }

        }

        /// <summary>
        /// 主キー設定
        /// </summary>
        public void ExcecuteSample07()
        {
            var realm = Realm.GetInstance();

            //Realmデータベースのパスを出力
            Debug.WriteLine(realm.Config.DatabasePath);

            realm.Write(() =>
            {
                //RealmにはAutoIncrementがないので UUID を主キーとして利用。
                realm.Add(new Dog { Id = Guid.NewGuid().ToString(),Name = "ポチ", Age = 5 });
                realm.Add(new Dog { Id = Guid.NewGuid().ToString(),Name = "クロ", Age = 2 });
                realm.Add(new Dog { Id = Guid.NewGuid().ToString(),Name = "サチ", Age = 3 });
            });

        }

        /// <summary>
        /// インデックス設定
        /// </summary>
        public void ExcecuteSample08()
        {
            var realm = Realm.GetInstance();

            //Realmデータベースのパスを出力
            Debug.WriteLine(realm.Config.DatabasePath);

            realm.Write(() =>
            {
                //RealmにはAutoIncrementがないので UUID を主キーとして利用。
                realm.Add(new Cat { Id = Guid.NewGuid().ToString(), Name = "タマ", Age = 5 });
                realm.Add(new Cat { Id = Guid.NewGuid().ToString(), Name = "ボチ", Age = 2 });
                realm.Add(new Cat { Id = Guid.NewGuid().ToString(), Name = "マル", Age = 3 });
            });

        }
    }
}
