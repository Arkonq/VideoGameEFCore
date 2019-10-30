using Domain;
using HighSchoolLesson.DataAccess;
using System;
using System.Linq;

namespace VideoGameEFCore
{
	/*
	Создать сущности Видеоигра, Пользователь и Рейтинг. Подлючить EF Core и с помощью Code First сгенерировать БД и таблицы. 	
	Добавить тестовые данные.	
	Таблица Рейтинг содержит оценку пользователя той или иной игры по 5-ти бальной шкале.	
	Написать код, который выводит постранично (по 3 элемента) название, описание игр и средний рейтинг игры в консоли.	
	Предоставить sql скрипт для заполнения таблиц для проверки.
	*/
	class Program
	{
		static void Main()
		{
			//CreateDbAndAddData();
			Show();
		}

		static void Show()
		{
			int gamesCount;
			int pages, pageSize = 3, page;

			using (var context = new Context())
			{
				gamesCount = context.Videogames.ToList().Count;
			}
			pages = gamesCount / pageSize;
			if (gamesCount % pageSize != 0)
			{
				pages++;
			}
			while (true)
			{
				Console.WriteLine($"Введите страницу (1 - {pages}; 0 - Выход):");

				if (Int32.TryParse(Console.ReadLine(), out page) == false || page > pages || page < 0)
				{
					page = 1;
					Console.WriteLine("Введен неверный номер страницы");
					Console.ReadLine();
					Console.Clear();
					continue;
				}

				if (page == 0) { return; }

				ShowPage(pageSize, page);
			}
		}

		private static void ShowPage(int pageSize, int page)
		{
			Console.Clear();
			Console.WriteLine($"Страница {page}:");
			using (var context = new Context())
			{
				var result = context.Videogames.OrderBy(x => x.Name).ToList();
				var pagingResult = result.Skip((page - 1) * pageSize).Take(pageSize);

				int num = (page - 1) * pageSize + 1;
				foreach (var game in pagingResult)
				{
					Console.WriteLine($"\t{num++})Видеоигра: {game.Name}");

					Console.WriteLine($"\tОписание:");
					Console.Write("\t");
					Console.WriteLine(DivideString(game.Description));

					var query = from rating
											in context.Ratings
											where rating.Videogame.Id == game.Id
											select rating;
					Console.WriteLine($"\tВсего оценок: {query.Count()}");


					double avrgRate = 0;
					foreach (var oneRate in query.ToList())
					{
						avrgRate += oneRate.Rate;
					}
					avrgRate /= (double)query.ToList().Count;

					Console.WriteLine($"\tОбщий рейтинг: {avrgRate.ToString("0.0")}");
					Console.WriteLine();
				}
			}
		}

		private static string DivideString(string str)
		{
			String[] sublines = str.Split(' ');
			str = null;
			int length = 90; //длина разбиения
			int j = 0;
			for (int i = 0; i < sublines.Count(); i++)
			{
				if (j + sublines[i].Length < length)
				{
					str = str + sublines[i] + " ";
					j = j + sublines[i].Length;
				}
				else
				{
					j = 0;
					str = str + "\r\n\t";
					i--;
				}
			}
			return str;
		}

		private static void CreateDbAndAddData()
		{
			using (var context = new Context())
			{
				var game1 = new Videogame
				{
					Name = "CS:Go",
					Description = "Просто жмешь ПКМ и радуешься жизни, чего сложного то"
				};
				var game2 = new Videogame
				{
					Name = "Dota 2",
					Description = "Лучшая стратегия со времен палеозоя. Для лучшей игры сменить язык интерфейса и регион с русского на какой-нибудь другой"
				};
				var game3 = new Videogame
				{
					Name = "Quake 3",
					Description = "Добро пожаловать на Арену, где высокопоставленные воины превращаются в беспозвоночную кашу. Отказываясь от каждой капли здравого смысла и любого следа сомнения, вы устремляетесь на арену душераздирающих пейзажей и завуалированных пропастей. Ваше новое окружение отвергает вас ямами лавы и атмосферными опасностями, в то время как вас окружают легионы врагов, испытывая первую реакцию, которая в первую очередь привела вас сюда. Ваша новая мантра: Сражайся или до свидания."
				};
				var game4 = new Videogame
				{
					Name = "Devil May Cry 5",
					Description = "Лучший охотник на демонов возвращается в новом стильном боевике. В пятой части легендарной серии Devil May Cry вы вновь сможете насладиться сверхскоростными сражениями с участием невероятных персонажей.Новейшие технологии компьютерной графики позволили Capcom создать этот непревзойденный шедевр жанра экшен."
				};
				var game5 = new Videogame
				{
					Name = "Doki Doki Literature Club!",
					Description = "Welcome to the Literature Club! It's always been a dream of mine to make something special out of the things I love. Now that you're a club member, you can help me make that dream come true in this cute game!"
				};
				var game6 = new Videogame
				{
					Name = "STAR WARS Jedi: Fallen Order",
					Description = "В «Звёздные Войны Джедаи: Павший Орден», боевике с видом от третьего лица от Respawn Entertainment, вас ждут приключения галактического масштаба. Действие этой сюжетной одиночной игры разворачивается после фильма «Эпизод III — Месть ситхов». Вам предстоит очутиться в роли джедая-падавана, которому едва удалось избежать уничтожения, санкционированного Приказом 66. В стремлении восстановить Орден джедаев вам придётся собрать воедино осколки прошлого, чтобы завершить обучение, обрести новые способности Силы и овладеть мастерством боя на легендарных световых мечах, а главное — оставаться при этом на шаг впереди Империи и её беспощадных инквизиторов."
				};

				var game7 = new Videogame
				{
					Name = "THE WITCHER 3: WILD HUNT",
					Description = "«Ведьмак 3: Дикая Охота» — это сюжетная ролевая игра с открытым миром. Её действие разворачивается в поразительной волшебной вселенной, и любое ваше решение может повлечь за собой серьёзные последствия. Вы играете за профессионального охотника на монстров Геральта из Ривии, которому поручено найти Дитя предназначения в огромном мире, полном торговых городов, пиратских островов, опасных горных перевалов и заброшенных пещер."
				};


				var user1 = new User
				{
					Name = "Ваня Пудж"
				};
				var user2 = new User
				{
					Name = "Александр"
				};
				var user3 = new User
				{
					Name = "Road to Global"
				};
				var user4 = new User
				{
					Name = "Dondo"
				};

				var rating1 = new Rating
				{
					Videogame = game1,
					User = user1,
					Rate = 4
				};
				var rating2 = new Rating
				{
					Videogame = game1,
					User = user2,
					Rate = 3
				};
				var rating3 = new Rating
				{
					Videogame = game1,
					User = user3,
					Rate = 3
				};
				var rating4 = new Rating
				{
					Videogame = game2,
					User = user1,
					Rate = 5
				};
				var rating5 = new Rating
				{
					Videogame = game2,
					User = user2,
					Rate = 4
				};
				var rating6 = new Rating
				{
					Videogame = game2,
					User = user3,
					Rate = 5
				};
				var rating7 = new Rating
				{
					Videogame = game2,
					User = user4,
					Rate = 5
				};
				var rating8 = new Rating
				{
					Videogame = game3,
					User = user3,
					Rate = 5
				};
				var rating9 = new Rating
				{
					Videogame = game4,
					User = user1,
					Rate = 5
				};
				var rating10 = new Rating
				{
					Videogame = game5,
					User = user2,
					Rate = 4
				};
				var rating11 = new Rating
				{
					Videogame = game6,
					User = user3,
					Rate = 4
				};
				var rating12 = new Rating
				{
					Videogame = game7,
					User = user4,
					Rate = 4
				};

				context.Add(game1);
				context.Add(game2);
				context.Add(game3);
				context.Add(game4);
				context.Add(game5);
				context.Add(game6);
				context.Add(game7);
				context.Add(user1);
				context.Add(user2);
				context.Add(user3);
				context.Add(user4);
				context.Add(rating1);
				context.Add(rating2);
				context.Add(rating3);
				context.Add(rating4);
				context.Add(rating5);
				context.Add(rating6);
				context.Add(rating7);
				context.Add(rating8);
				context.Add(rating9);
				context.Add(rating10);
				context.Add(rating11);
				context.Add(rating12);

				context.SaveChanges();
			}
		}
	}
}
