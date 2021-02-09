using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace projectx1
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class userProfile : ContentPage
	{
        List<string>  same= new List<string>();
        List<string> sellected = new List<string>();
        public List<int> included = new List<int>();
        public List<int> indexOfImag = new List<int>();
        public Image[] image= new Image[20];
        public readonly Random random = new Random();
        public string[] imgs =
        { 
         "x.png","download.png","download1.png","luffy1.png","onePice1.png","sao1.png","sao2.png","zoro1.png","zoro2.png","zoro3.png"
        };
        System.Timers.Timer _timer = new System.Timers.Timer();
        public bool reseat = false;
        bool  firstGuss =true;
        int tapCount = 0;
        int allowClick = 0;
        int score = 0;
        int _countSeconds;
        public userProfile ()
		{ 
			InitializeComponent ();

             image[0] = btn1;
             image[1] = btn2;
             image[2] = btn3;
             image[3] = btn4;
             image[4] = btn5;
             image[5] = btn6;
             image[6] = btn7;
             image[7] = btn8;
             image[8] = btn9;
             image[9] = btn10;
             image[10] = btn11;
             image[11] = btn12;
             image[12] = btn13;
             image[13] = btn14;
             image[14] = btn15;
             image[15] = btn16;
             image[16] = btn17;
             image[17] = btn18;
             image[18] = btn19;
             image[19] = btn20;

            setRandomImage();
            showImages();

          
        }

        
        private  void Repeat_Clicked(object sender, EventArgs e)
        {
            included.Clear();
            indexOfImag.Clear();
            sellected.Clear();
            same.Clear();
            setRandomImage();
            showImages();
            score = 0;
            tapCount = 0;
            allowClick = 0;
            firstGuss = true;
            reseat = false;
        

    }

        async void  OnTapGestureRecognizerTapped(object sender, EventArgs args)
        {
           
          //  tapCount++;
            var imageSender = (Image)sender;
        
            
               //if (tapCount==2)
                   {
                   for (int i = 0; i < image.Length; i++) {
                   if (imageSender.BindingContext.ToString() ==image[i].BindingContext.ToString()) {
                     imageSender.Source = imgs[indexOfImag.ElementAt(i)];
                     imageSender.BackgroundColor = Color.White;
                        if (firstGuss)
                        {
                             same.Add((i.ToString()));
                            same.Add(imageSender.Source.ToString());
                            same.Add(image[i].BindingContext.ToString());
                           firstGuss = false;
                        }
                        else if(same.Contains( imageSender.Source.ToString())&& ! same.Contains( imageSender.BindingContext.ToString() ))
                        { if (!sellected.Contains(imageSender.Source.ToString()))
                            {
                                sellected.Add(imageSender.Source.ToString());
                                score++;
                                winCase();
                                same.Clear();
                                firstGuss = true;
                                Score.Text = score.ToString();
                            }
                        }else
                        {

                            await Task.Delay(1000);
                       

                            image[Int32.Parse(same.First())].Source = "#";
                            image[Int32.Parse(same.First())].BackgroundColor = Color.Black;

                            imageSender.Source = "#";
                            imageSender.BackgroundColor = Color.Black;
                            same.Clear();
                            firstGuss = true;
                        }
                                          break;
                    }
                }
        }
            //else
            //{
            //    imageSender.Source = "#";
            //    imageSender.BackgroundColor=Color.Black;
            //}
        }

        private  void   Again_Clicked(object sender, EventArgs e)
        {
            allowClick++;
            if (allowClick <= 3)
            {
                showImages();
            }
            else
            {
                again.IsEnabled = false;
            }


         
        }
        public async void showImages()
        {
            for (int i = 0; i < image.Length; i++)
            {
                    image[i].Source = imgs[indexOfImag.ElementAt(i)];
                    image[i].BackgroundColor = Color.White; 

            }
            await Task.Delay(4000);
            for (int i = 0; i < image.Length; i++)
            {
                if (!sellected.Contains(image[i].Source.ToString()))
                {
                    image[i].Source = "#";
                    image[i].BackgroundColor = Color.Black;
                }
            }
        }
        public  void setRandomImage()
        { int check= 0;
           
            for (int i=0;i<image.Length;i++)
            {  bool found = false;
                bool founddouple = false;
                var index=0;
                do {
                     index = random.Next(0, imgs.Length);
                     founddouple = false;
                    for (int j = 0; j < included.Count; j++)
                    {
                        if (index == included.ElementAt(j) )
                        {
                            founddouple = true;
                            break;
                        }
                      
                    }
                } while (founddouple);
                for (int j = 0; j < indexOfImag.Count; j++)
                { if ((index == indexOfImag.ElementAt(j) && check<1))
                    {
                        indexOfImag.Add(index);
                        check++;
                        found = true;
                        break;
                    }
                }
               
                if (!founddouple && check == 1)
                {
                    included.Add(index);
                    check = 0;
                    founddouple = true;

                }
                if (!found&&!founddouple  )
                    {
                        indexOfImag.Add (index);
                      
                    }
                
            }
        }
       
        public async void winCase()
        {
            if (score == 10)
            {
                bool answer = await DisplayAlert("You Win", "Would you like to play a game", "Yes", "No");
                if (answer)
                {
                    await Task.Delay(1000);
                    included.Clear();
                    indexOfImag.Clear();
                    sellected.Clear();
                    same.Clear();
                    setRandomImage();
                    showImages();
                    score = 0;
                    tapCount = 0;
                    allowClick = 0;
                    firstGuss = true;
                    reseat = false;
    }
                else
                {
                    System.Diagnostics.Process.GetCurrentProcess().Kill();

                }

            }
        }
    }
}