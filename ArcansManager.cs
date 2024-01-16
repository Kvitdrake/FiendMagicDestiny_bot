﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiendMagicDestiny_bot
{
    internal class ArcansManager
    {
        private Dictionary<short, TaroArcans> Arcans = new Dictionary<short, TaroArcans>()
        {
            {1, new TaroArcans ("   МАГ", $"{Desc.Mag} МУЖЧИНА – МАГ:\r\nОсознанное становление на путь Воина. Лидер. Активный и цельный. Крайность – самоуверенность.\r\n",
                               $"{Desc.Mag} ЖЕНЩИНА – МАГ:\r\nЖенщина-калейдоскоп. Всегда способна удивлять. Высокий интеллект. Разум, индивидуальность и независимость. Нет эмоциональной привязки к чему-либо.\r\n") },
            { 2, new TaroArcans ("   ЖРИЦА", $"{Desc.Pappet} МУЖЧИНА – ЖРИЦА:\r\nЭтот Аркан пассивный, но даёт очень глубокую интуицию. Такие мужчины будут точны в своих наблюдениях-ощущениях. Почувствуют и объяснят. Очень заботливые.\r\n",
                                    $"{Desc.Pappet} ЖЕНЩИНА – ЖРИЦА:\r\nДля женщин этот Аркан крайне положителен. Дает женские состояния принятия, интуиции, чувственности. Проницательные натуры. Женщина-жрица гармонично может находиться на втором плане, но она знает свою силу. Это пассивный Аркан, но женщине-Жрице и не нужно быть активной.\r\n") },
        { 3, new TaroArcans ("   ИМПЕРАТРИЦА", $"{Desc.Empress} МУЖЧИНА – ИМПЕРАТРИЦА:\r\nМетросексуалы, нежные и лощеные мужчины. Любят деньги и комфорт. В минусе – это женственный мужчина, который перекладывает ответственность на других. Сильная зависимость от женщин. Мужчины, которые выросли с властными матерями, будут иметь искаженный вариант Императрицы. И сила этого Аркана пойдет не на творчество и реализацию, а на подкаблучничество перед женщинами.\r\n",
                                          $"{Desc.Empress} ЖЕНЩИНА – ИМПЕРАТРИЦА:\r\nЯркий Аркан женственности, материнства и красоты, выигрышный Аркан в портрете женщины. Роскошь. Таким женщинам крайне важно, чтобы муж хорошо зарабатывал. Женщина-Императрица ценит себя. В минусе женщины-Императрицы увлекаются продажей себя. Вульгарность и излишества в стиле. Перебор в пластических операциях.\r\n") },
            { 4, new TaroArcans ("   ИМПЕРАТОР", $"{Desc.Emperor} МУЖЧИНА – ИМПЕРАТОР:\r\nВластный мужчина-собственник. Кредо Императора: “Правлю путем служения, служу путем правления”. Власть на благо людям. Патриотизм. Мужчины, которые берут ответственность за семью, род, страну. В минусе этот Аркан дает жесткость, бескомпромиссность, категоричность, невосприимчивость.\r\n",
                                        $"{Desc.Emperor} ЖЕНЩИНА – ИМПЕРАТОР:\r\nЖенщина-Император идёт в бизнес, в управление. Император дает женщине устойчивость, твердость, стремление к деньгам. Такая женщина умеет зарабатывать. Очень ответственна и в семье на нее всегда можно положиться. В минусе – это баба-мужик, которая все на себе тащит. Ломовая лошадь. Также Император в негативе дает женщине категоричность: женщина с костным мозгом, тупое упрямство в спорах.\r\n")},
            { 5, new TaroArcans("   ИЕРОФАНТ", $"{Desc.Hierophant} МУЖЧИНА – ИЕРОФАНТ:\r\nРуководители, которые для своих подчиненных являются наставником. Властный, но мудрый мужчина. К нему прислушиваются. Мужчины-Иерофанты отличаются благожелательностью: они действительно добры к людям. В минусе Иерофант даёт мужчинам категоричность, гордыню и высокомерие.\r\n",
                                       $"{Desc.Hierophant} ЖЕНЩИНА – ИЕРОФАНТ:\r\nЖенщины-учителя, которые и сами по жизни будут постоянно стремиться к новым знаниям, может быть несколько высших образований. Такие женщины могут серьезно увлекаться религией, эзотерикой, проработками различными. Очень эффективные и понимающие наставники. С ними приятно и полезно общаться. Свои знания они тасуют, как карточную колоду, и в каждый момент жизни у них есть емкое и точное определение: или притча, или анекдот, или стих, или цитата. В минусе – приоритет ставится на мораль и знания, а не на любовь и чувства.\r\n")},
        {6, new TaroArcans ("   ВЛЮБЛЁННЫЕ", $"{Desc.Lovers} МУЖЧИНА – ВЛЮБЛЕННЫЕ: \r\nПритягательные и обаятельные. Вокруг них всегда много женщин. Такие мужчины теплы в общении. Но в минусе – это болтливые ловеласы и альфонсы. Не в состоянии хранить верность одной. Постоянно перебирают партнерш. В проработке Аркан Влюбленные дает мужчине способность глубоко любить одну женщину, то есть в своей спутнице он видит всех женщин мира. И ему не нужно распыляться, куда-то налево уходить.\r\n",
                                         $"{Desc.Lovers} ЖЕНЩИНА – ВЛЮБЛЕННЫЕ: \r\nДевушки с Арканом Влюбленные порхают от одного мужчины к другому. Перебирают мужчин как товар. Коммуникабельные и обворожительные женщины. Умеют подать себя. Женщины-интеллектуалки. Салонные поэтессы. Творческие натуры, но настроены на любовные отношения. Могут блеснуть эрудицией, но часто эти знания поверхностны. Это не Аркан глубины. Могут быть сложности в выборе одного и единственного мужчины. Много поклонников. Задача женщины с этим Арканом – принимать мужчину целостно, его черную и белую сторону.\r\n")},
            { 7, new TaroArcans("   КОЛЕСНИЦА", $"{Desc.Chariot} МУЖЧИНА – КОЛЕСНИЦА:\r\nПобедитель. Лидер. Управленец. Постоянное движение и развитие. Очень активный и драйвовый мужчина. Не сидит на одном месте. Постоянные поездки, в том числе заграничные. С такими людьми крайне интересно делать бизнес. Любовь к любым видам транспорта, к вождению. В минусе – человек захлебывается в своих эмоциях: мужчина-истеричка.\r\n",
                                        $"{Desc.Chariot} ЖЕНЩИНА – КОЛЕСНИЦА:\r\nЛихачка. Нацелена на карьеру и развитие. Склонна к тщеславию. Женщина-Колесница приятна и интересна в общении. Обладает широким кругозором. Такая женщина любит поездки, перелеты, путешествия. Легка на подъем. В минусе Аркан дает эмоциональную незрелость: женщина не справляется со своими эмоциями, истерит и психует. Наличие пустых амбиций.\r\n") },
        {8, new TaroArcans("   ПРАВОСУДИЕ", $"{Desc.Justice} МУЖЧИНА – ПРАВОСУДИЕ: \r\nПроработанный Аркан Правосудие дает мужчине объективность и непредвзятость: он над ситуацией, эмоционально не вовлекается. Выдача информации из состояния наблюдения, а не причастности. В минусе – очень критичные люди. Дают всему и вся язвительные комментарии. Высокомерие.\r\n",
                                         $"{Desc.Justice} ЖЕНЩИНА – ПРАВОСУДИЕ: \r\nЖенщина-интеллектуалка. Наблюдательная и разумная. Может успешно реализоваться в бизнесе. В минусе – критичное отношение к окружающим. Осуждение. Бескомпромиссная критика ради самой критики, а не ради того, чтобы человек менялся.\r\n")},
        {9, new TaroArcans("   ОТШЕЛЬНИК", $"{Desc.Hermit} МУЖЧИНА – ОТШЕЛЬНИК: \r\nМужчина с таким Арканом спокойный и рассудительный. У него своя система ценностей. Это не человек толпы. Он может вести какой-то проект сам. Спокойно и без суеты. Отшельник дает мужчинам состояние глубины, а также внутреннюю красоту и мудрость. Мужчина-Отшельник может дать ценный совет. Долго ищет свою половинку, так как крайне внимателен и разборчив в выборе.\r\n",
                                        $"{Desc.Hermit} ЖЕНЩИНА – ОТШЕЛЬНИК: \r\nИндивидуалистка. Может идти против толпы и принятых в обществе норм. Независимая от чужого мнения женщина. Мудрость и глубина, сосредоточенность и концентрация на внутреннем содержании. Зрит в корень. Видит ситуацию изнутри. Большое удовольствие общаться с женщиной, у которой проработан \r\n")},
        { 10, new TaroArcans("   КОЛЕСО ФОРТУНЫ", $"{Desc.WheelOfFortune} МУЖЧИНА – КОЛЕСО ФОРТУНЫ:\r\nВот есть мужчины, которые каждый миллиметр своего успеха зарабатывают тяжким трудом, а есть те, которым просто повезло. Это Аркан Колесо Фортуны: “проснулся знаменитым”. Попадание в нужное время в нужное место. Активные мужчины, много путешествует. В минусе – это глупый азарт и не обоснованный риск, когда человек ставит на кон все, что у него есть.\r\n",
                                              $"{Desc.WheelOfFortune} ЖЕНЩИНА – КОЛЕСО ФОРТУНЫ:\r\nТакой женщине часто будет везти по жизни: на людей, на ситуации. Фартовость и легкое отношение к жизни: что-то не получилось? Да ну и ладно. Оптимизм, коммуникабельность, хорошее настроение. В минусе – раздолбайство на основе изолированности от реальности. Думает о насущном от случая к случаю.\r\n")},
        { 11, new TaroArcans("   СИЛА", $"{Desc.Strength} МУЖЧИНА – СИЛА:\r\nМужчинам Аркан Сила дает чистое лидерство. Настоящий лидер в душе. Это полководцы, ведущие за собой. Ключевое слово – широта души, благородство. Мужчины – покровители. Не считает мелочь, щедрый. Возвышенный в хорошем смысле этого слова. В минусе – это насилие и агрессия. Физическое применение своей силы.\r\nПервый аркан страсти - это Сила. Люди с этим арканом любят выяснять отношения , скандалить, бить посуду! Мужчины по этому аркану любят красивые благородные жесты: покупают своим партнершам меха, бриллианты не глядя на цену. Такой парадный фасад. Пример - Паратов из \"Жестокого романса\": прокутить все деньги с цыганами, заложить пароход. Мужчины с Силой любят власть. Мачомены. Любят страстных женщин и страстное выяснение отношений. Серые мыши им не интересны. \r\n",
                                    $"{Desc.Strength} ЖЕНЩИНА – СИЛА:\r\nЯркие женщины. Женственность через сексуальность. Мощная энергетика. Власть и красота. За такой женщиной хочется идти: она знает куда, зачем, и что надо делать. У таких женщин очень теплая и притягательная энергетика. В минусе – давящая аура. Желание, чтоб за тобой “побегали”. Категоричная и жесткая женщина.\r\nЖенщины с арканом Сила любят проявить свою сексуальность и красиво себя подать. Пример - Моника Белуччи с 2-мя арканами Сила, Дженифер Лопес. Женщины, которые знают себе цену, своей харизме, чувствуют свою власть над мужчинами, и умеют себя подать в нужном ракурсе. \r\n")},
        { 12, new TaroArcans("   ПОВЕШЕННЫЙ", $"{Desc.HangedMan} МУЖЧИНА - ПОВЕШЕННЫЙ:\r\n Мужчинам Аркан Повешенный дает чуткость. Глубокое чувствование другого человека и мира в целом. Тонкая сонастройка. Таланты в искусстве. В минусе - человек оторван от реальности. Жизнь в собственном мире. Будет избегать ответственности и фактов. Ему показывают урок для проработки – а он сбегает.\r\n",
                                          $"{Desc.HangedMan} ЖЕНЩИНА - ПОВЕШЕННЫЙ:\r\n Аркан Повешенный – это иньский Аркан и он, конечно же, очень подходит женщинам. Дает девушкам чувствительность, глубину, понимание нюансов. Тонкая душевная организация. Милосердие в высшей форме. Неспешность в поведении. Богемная натура. В минусе – женщина впадает в иллюзии, избегает реальности. Оправдывает других людей. Женщина-жертва. Не берет на себя ответственность.\r\n")},
        { 13, new TaroArcans("   СМЕРТЬ", $"{Desc.Death} МУЖЧИНА – СМЕРТЬ:\r\nКлючевое слово для мужчин с Арканом Смерть – магнитичность. К нему тянешься, как к магниту. Независимые люди. Испытывают крайности. В проработанном состоянии Аркан Смерть дает мужчинам бесстрашие. Четкое осознание, что есть жизнь после смерти. Что смерть – это не конец, а начало нового пути. В минусе – безбашенность, риск ради риска.\r\n",
                                      $"{Desc.Death} ЖЕНЩИНА – СМЕРТЬ:\r\nРоковая женщина. Рядом с такой девушкой ты трансформируешься, перерождаешься. Постоянное развитие. Глубоко видит суть человека. Независимость от материального и каких-либо социальных устоев. В минусе – постоянные ситуации на грани жизни и смерти.\r\n")},
        { 14, new TaroArcans("   УМЕРЕННОСТЬ", $"{Desc.Temperance} МУЖЧИНА – УМЕРЕННОСТЬ: Люди не быстрого мышления: все взвесят, обдумают. Это не спонтанность. На таких мужчин приятно смотреть: ухоженные и уравновешенные. В минусе – стремление к перееданию, бросается из крайности в крайность. Пассивность.\r\n",
                                           $"{Desc.Temperance} ЖЕНЩИНА – УМЕРЕННОСТЬ: Если у женщины проработан Аркан Умеренность, то рядом с ней психологически очень комфортно. Она спокойна и добра. Мягкая и приятная энергетика. В минусе – неуравновешенность, истерики, лишний вес, лень.\r\n")},
        { 15, new TaroArcans("   ДЬЯВОЛ", $"{Desc.Devil} МУЖЧИНА - ДЬЯВОЛ:\r\nСоблазнители - искусители. Способность к манипулированию. Хитрость. Маги тоже склонны к манипуляциям, но Дьявол более изощрен в этом. Женщины табунами охотятся за такими мужчинами. Часто крупные руководители имеют Аркан Дьявол. Такие мужчины очень любят деньги. В минусе: рассеивание сексуальной энергии направо и налево. Количество, а не качество.\r\nМужчины с Арканом Дьявола – это скорее, всемирная власть, что-то типа Адольфа Гитлера.\r\n",
                                      $"{Desc.Devil} ЖЕНЩИНА - ДЬЯВОЛ:\r\nЗадорная, энергичная и авантюрная. Женщина-Дьявол стремится к материальному успеху и карьере. Лидерство. Любит блистать в обществе. Роковая соблазнительница. Любит сексуальные эксперименты. В минусе: эго лезет изо всех щелей. Коллекционирует мужчин. Яркий пример – Настасья Филипповна из романа Достоевского “Идиот”: богатая содержанка, ради которой мужчины совершают безумные поступки.\r\n\r\nДля женщины с Арканом Дьявол секс может быть способом заработка. Содержанки, куртизанки.\r\n")},
        { 16, new TaroArcans("   БАШНЯ", $"{Desc.Tower} МУЖЧИНА – БАШНЯ:\r\nУ таких мужчин очень жесткая внутренняя структура. Настоящий стержень и каркас. Смелые и отчаянные. По своей мощи Аркан Башня похож на Аркан Император. В минусе – до них трудно что-то донести. Мир ломает таких людей, чтоб они стали менее ограниченными, учились быть более гибкими: “разрушь моё сердце, чтобы я почувствовал настоящую любовь”.\r\n",
                                     $"{Desc.Tower} ЖЕНЩИНА – БАШНЯ:\r\nДевушки с четкой жизненной позицией. Сильные и независимые личности. Любят власть. Прямолинейны. Разрушают все искусственное вокруг. В минусе – стремление к разрушению себя. Находясь в Башне – находятся в самоограничении. Подавляют мужчин. Делают подкаблучниками. Энергетика такая, что мужчине сложно находится в длительных отношениях с такой женщиной.\r\n")},
        { 17, new TaroArcans("   ЗВЕЗДА", $"{Desc.Star} МУЖЧИНА – ЗВЕЗДА:\r\nТакой мужчина всегда будет выделяться из толпы. Стремиться к вниманию и известности. Часто популярен в той или иной сфере. Могут обладать экстрасенсорными способностями. В минусе – закрытые и холодные мужчины. Звездная болезнь. Зацикленность на самом себе.\r\n",
                                      $"{Desc.Star} ЖЕНЩИНА – ЗВЕЗДА:\r\nБлистают в обществе. Даже в одежде могут быть очень яркие элементы, которые подчеркнут ее особенность. Внешняя привлекательность. Увлекаются эзотерикой, астрологией, биоэнергетикой. Творческие девушки. В минусе – безвкусица и эгоцентризм.\r\n")},
        { 18, new TaroArcans("   ЛУНА", $"{Desc.Moon} МУЖЧИНА - ЛУНА:\r\nПроработанный Аркан Луна дает мужчине ясность и искренность. Творческое мышление и умение с любым человеком найти общий язык. В минусе – мужчина ведет двойную жизнь. Мутные персонажи. Изворотливые. Добиться правды от мужчины-Луны очень сложно.\r\n",
                                    $"{Desc.Moon} ЖЕНЩИНА - ЛУНА:\r\nАркан Луна - Аркан женского архетипа. Материнство. Дает девушке чувствительность. Эмпатия. Способности к бытовой магии. В минусе - туманная личность. Двойные стандарты. Такие женщины могут стремиться к управлению через манипуляции.\r\n")},
        { 19, new TaroArcans("   СОЛНЦЕ", $"{Desc.Sun} МУЖЧИНА – СОЛНЦЕ: \r\nДобрые и заботливые мужчины. Открыты и теплы. Часто занимают крупные посты. Социально реализованные. Всегда привлекают внимание. За ними хочется идти. Они просто естественным образом становятся лидерами. Солнце – планета Льва – царя зверей. Благородны и щедры. В минусе – инфантильность и безответственность.\r\n",
                                      $"{Desc.Sun} ЖЕНЩИНА – СОЛНЦЕ: \r\nЯркие и солнечные девушки. С ними приятно общаться: заряжают позитивом. Cтремятся быть в центре внимания, но не любой ценой. Часто бывают неформальными лидерами, так как с ними комфортно и интересно. Очень любят детей. У многих творческих людей есть Аркан Солнце. Сам по себе очень добрый Аркан, но в минусе дает оторванность от реальности. Не задумываются над серьезностью последствий своих необдуманных решений. Эгоцентризм.\r\n")},
        { 20, new TaroArcans("   СУД", $"{Desc.Judgement} МУЖЧИНА – СУД: \r\nТотальный рентген. Он видит тебя насквозь. Сканирующий взгляд. Такие мужчины всегда будут интересоваться глубиной жизни и смерти, крайностями и масштабностью. Крупные полководцы зачастую имеют в портретной характеристике Аркан Суд. Считается, что мужчина-суд расплачивается за родовую карму. Также такой человек может дать начало настоящему семейному клану. В минусе – жесткое давление, диктаторство.\r\n",
                                   $"{Desc.Judgement} ЖЕНЩИНА – СУД: \r\nРоковые женщины. Власть над мужчинами. Влиятельны. Способны делать разумные выводы из своих ошибок. Стремятся к постоянному профессиональному росту. Объединяют вокруг себя всю семью. Родовое гнездо. Очень глубинные женщины с чувством собственного достоинства. В минусе – травмирующие события, кризисы и проблемы в семье.\r\n")},
        { 21, new TaroArcans("   МИР", $"{Desc.World} МУЖЧИНА – МИР: \r\nНаблюдательные, спокойные и коммуникабельные мужчины. Возможна склонность к гомосексуальным отношениям. Способны целостно принимать человека.\r\n",
                                   $"{Desc.World} ЖЕНЩИНА – МИР: \r\nГармоничные и цельные натуры. Они принимают жизнь, такой какая она есть. Видят во всем связь и единство. Толерантны ко всем религиям. Увлекаются культурой других стран. Много путешествуют.\r\n")},
        { 22, new TaroArcans("   ШУТ", $"{Desc.Fool} МУЖЧИНА – ШУТ:\r\nМужчина с Шутом может быть весельчаком, душой компании. Это человек, который не привязан к материальному, но который спонтанен в плане заработка. Поработал в одном месте – ушел, поменял сферу деятельности, занялся чем-то новым.\r\n",
                                   $"{Desc.Fool} ЖЕНЩИНА – ШУТ:\r\nСумасбродка. Девочка-праздник. Маленький ребенок. Это фантазерка, которая живет в собственном мире грёз. Оригинальное мышление. Многие клоунессы и комедийные актрисы идут по Шуту. Это женщины, которые могут смешить. Творческая свобода. Идут своим путём, не смотря ни на что. Это не женщина ума, это женщина сердца. Непредсказуемая и забавная. Это та, с которой интересно в компании. Сложно представить женщину-Шута как примерную жену, домохозяйку. Это возможно в том случае, если в портрете есть материальные Арканы, которые заземлят её. В минусе – инфантильность. Могут впадать в зависимость от кого-то.\r\n")}
        };
        public TaroArcans GetArcan(short number)
        {
            if(Arcans.ContainsKey(number))
            {
               return Arcans[number];
            }
            else
            {
                return null;
            }
        }
    }
}