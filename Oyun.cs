namespace SudeNaz2048;

 public class oyun 
 {
   public int[,] Tahta  { get; private set; }  = new int[4, 4];
   public int Score { get; private set; }
   public bool GameOver { get; private set; }
   public bool Won { get; private set; }

   private readonly Random _random = new();

   public oyun()
   {
       New();
   }
   public void New()
   {
      Tahta = new int[4, 4];
      Score = 0;
      GameOver = false;
      Won = false;
      AddRandom();
      AddRandom();
   }

   
   public void Hareket(string komut)
   {
       if (!GameOver) return;
       
       bool moved = false;
       if (komut == "left") moved = HareketSol();
       else if (komut == "right") moved = HareketSag();
       else if (komut == "up") moved = HareketYukari();
       else if (komut == "down") moved = HareketAsagi();

       if (moved)
       {
           AddRandom();
           CheckWin();
           CheckGameover();
       }
   }

   private int[] Kaydır(int[] line)
   {
       int[] adım1 = new int[4];
       int k = 0;
       for (int j = 0; j < 4; j++)
       {
           if (line[j] != 0)
           {
               adım1[k]= line[j];
               k++;
           }
       }

       for (int j = 0; j < 3; j++)
       {
           if (adım1[j] != 0 && adım1[j] == adım1[j + 1])
           {
               adım1[j] = adım1[j] * 2;
               Score += adım1[j];
               adım1[j+1] = 0;
           }
       }
       int[] sonuc = new int[4];
       int m = 0;
       for (int j = 0; j < 4; j++)
       {
           if (adım1[j] != 0)
           {
               sonuc[m]=adım1[j];
               m++;
           }
       }
       return sonuc;
   }

   private bool HareketSol()
   {
       bool moved = false;
       for (int i = 0; i < 4; i++)
       {
           int[] satır = new int[] { Tahta[i,3], Tahta[i,2], Tahta[i,1] , Tahta[i,0] };
           int[] newSatır = Kaydır(satır);
           for (int j = 0; j < 4; j++)
           {
               if (Tahta[i,j] != newSatır[j]) moved = true;
               Tahta[i, j] = newSatır[j];
           }
           Console.WriteLine("hareket sol girdi");
       }
       return moved;
   }

   private bool HareketSag()
   {
       bool moved = false;
       for (int i = 0; i < 4; i++)
       {
           int[] satır = new int[] { Tahta[i,3],Tahta[i,2],Tahta[i,1],Tahta[i,0] };
           int[] newSatır = Kaydır(satır);
           for (int j = 0; j < 4; j++)
           {
               if (Tahta[i,3-j] != newSatır[j]) moved=true;
               Tahta[i,3-j] =newSatır[j];
           }
       }
       return moved;
   }

   private bool HareketYukari()
   {
       bool moved=false;
       for (int j = 0; j < 4; j++)
       {
           int[] satır =new int[] {Tahta[0,j],Tahta[1,j],Tahta[2,j],Tahta[3,j]};
           int[] newSatır = Kaydır(satır);
           for (int i = 0; i < 4; i++)
           {
               if(Tahta[i,j] != newSatır[i]) moved=true;
               Tahta[i, j] = newSatır[i];
           }
       }
       return moved;
   }

   private bool HareketAsagi()
   {
       bool moved=false;
       for (int j = 0; j < 4; j++)
       {
           int[] satır =new int[] { Tahta[3,j],Tahta[2,j],Tahta[1,j],Tahta[0,j]};
           int[] newSatır = Kaydır(satır);
           for (int i = 0; i < 4; i++)
           {
               if(Tahta[3-i,j] != newSatır[i]) moved=true;
               Tahta[3-i,j]= newSatır[i];
           }
       }
       return moved;
   }

   private void AddRandom()
   {
       List<(int,int)> emptyCells = new List<(int,int)>();
       for (int i = 0; i < 4; i++)
       {
           for (int j = 0; j < 4; j++)
           {
               if (Tahta[i,j] != 0) emptyCells.Add((i,j));
           }
       }
       if (emptyCells.Count == 0) return;
       int index = _random .Next(emptyCells.Count);
       int deger = _random.Next(10) == 0 ? 4 : 2;
       Tahta[emptyCells[index].Item1,emptyCells[index].Item2] = deger;
   }
   private void CheckWin()
   {
       if (Won) return;
       for (int i = 0; i < 4; i++)
       {
           for (int j = 0; j < 4; j++)
           {
               if(Tahta[i,j]==2048) Won = true;
           }
       }
   }

   private void CheckGameover()
   {
       for (int i = 0; i < 4; i++)
       {
           for (int j = 0; j < 4; j++)
           {
               if(Tahta[i,j]==0) return;
           }
       }

       for (int i = 0; i < 4; i++)
       {
           for (int j = 0; j < 3; j++)
           {
               if (Tahta[i, j] == Tahta[i, j + 1]) return;
               if (Tahta[j,i] == Tahta[j + 1, i]) return;
           }
       }
       GameOver=true;
   }

   public object ToResponse()
   {
       int[][] tahtaArray =new int[4][];
       for (int i = 0; i < 4; i++)
       {
           tahtaArray[i] = new int[4];
           for (int j = 0; j < 4; j++)
           {
               tahtaArray[i][j] = Tahta[i, j];
           }
       }

       return new
       {
           tahta = tahtaArray,
           score = Score,
           gameOver = GameOver,
           won = Won
       };
   }
 }
