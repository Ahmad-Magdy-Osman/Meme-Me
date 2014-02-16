using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using MemeMe.Resources;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Phone.Tasks;

namespace MemeMe
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();																										//Basic Calls For Starting The Application From The Base System
            OnStart();																													//Calling First Initializations Function
        }

        //First Initializations
        private void OnStart()
        {
            AppBar();																													//Calling Application Bar Function
            LoadMemeButton();																											//Loading First Category Memes(Default Arguments)
            LoadCategoryButtons();																										//Loading Categories Buttons
        }

        //Application Initializations
        private void AppBar()
        {
            ApplicationBar = new Microsoft.Phone.Shell.ApplicationBar();																//Initializing The Application Bar
            ApplicationBar.Mode = ApplicationBarMode.Default;																			//Application Bar Mode
            ApplicationBar.Opacity = 1.0;																								//Application Bar Opacity
            ApplicationBar.IsVisible = true;																							//Is Application Bar Visible?
            ApplicationBar.IsMenuEnabled = false;																						//Is Menu Bar Enabled?
            ApplicationBar.BackgroundColor = System.Windows.Media.Colors.Black;															//Application Bar Background
            ApplicationBar.ForegroundColor = System.Windows.Media.Colors.Orange;														//Application Bar Foreground

            ApplicationBarIconButton OpenFile = new ApplicationBarIconButton();															//Application Bar Iconic Button For Opening Images
            OpenFile.IconUri = new Uri("/Assets/AppBar/White/open.png", UriKind.Relative);												//Setting The Open File Button Image
            OpenFile.Text = "Open";																										//Initializing Its Text
            ApplicationBar.Buttons.Add(OpenFile);																						//Adding Open File Iconic Button To Application Bar
            OpenFile.Click += new EventHandler(OpenClicked);																			//Giving The Open File Button An Event Handler For Clicks

			ApplicationBarIconButton Snapshot = new ApplicationBarIconButton();															//Application Bar Iconic Button For Taking Pictures
			Snapshot.IconUri = new Uri("/Assets/AppBar/White/snapshot.png", UriKind.Relative);											//Setting The Snapshot Button Image
			Snapshot.Text = "Snapshot";																									//Initializing Its Text
			ApplicationBar.Buttons.Add(Snapshot);																						//Adding Snapshot Iconic Button To Application Bar
			Snapshot.Click += new EventHandler(SnapshotClicked);																		//Giving The Snapshot Button An Event Handler For Clicks

			ApplicationBarIconButton SaveFile = new ApplicationBarIconButton();															//Application Bar Iconic Button For Saving Images
			SaveFile.IconUri = new Uri("/Assets/AppBar/White/save.png", UriKind.Relative);												//Setting The Save File Button Image
			SaveFile.Text = "Save";																										//Initializing Its Text
			ApplicationBar.Buttons.Add(SaveFile);																						//Adding Save File Iconic Button To Application Bar
			SaveFile.Click += new EventHandler(SaveClicked);																			//Giving The Save File Button An Event Handler For Clicks

			ApplicationBarIconButton Share = new ApplicationBarIconButton();															//Application Bar Iconic Button For Sharing Images
			Share.IconUri = new Uri("/Assets/AppBar/White/share.png", UriKind.Relative);												//Setting The Share Image Button Image
			Share.Text = "Share";																										//Initializing Its Text
			ApplicationBar.Buttons.Add(Share);																							//Adding Share Iconic Button To Application Bar
			Share.Click += new EventHandler(ShareClicked);																				//Giving The Share Button An Event Handler For Clicks

			ApplicationBarMenuItem RateMyApplication = new ApplicationBarMenuItem();													//Application Bar Menu Item For Rating The Applications
			RateMyApplication.Text = "Rate & Review App";																				//Initializing The Text For The Menu Item
			ApplicationBar.MenuItems.Add(RateMyApplication);																			//Adding Rate Application Menu Item To The Application Bar
			RateMyApplication.Click += new EventHandler(RateClicked);																	//Giving Rate My Application Menu Item An Event Handler For Clicks
        }

		//Categories Buttons Initializations
        private void LoadCategoryButtons()
        {
			//Category Buttons List For Indexing
			List<Button> CategoryButtons = new List<Button>();																			

			//Constants & Variables Initializations
			int NumberOfCategories = 15;																					            //Number Of Categories Buttons To Be Loaded

			string[] NamesAndContents = new string[15] { "Angry", "Clothes", "Determined", "Disgusted", "Fap",							//String Array Of Names And Contents For Each Category Button
                                                         "Happy", "Me Guesta", "Misc", "Neutral", "Rage", 
                                                         "Sad", "Surprised", "Trees", "Troll", "Yao Ming" };

			var CategoryButtonColor = new LinearGradientBrush { EndPoint = new Point(0.5, 1), StartPoint = new Point(0.5, 0) };         //Coloring System For Category Buttons
			CategoryButtonColor.GradientStops.Add(new GradientStop { Color = Color.FromArgb(255, 83, 3, 3), Offset = 0.074 });          //Adding 1st Gradient Stop
			CategoryButtonColor.GradientStops.Add(new GradientStop { Color = Color.FromArgb(255, 212, 126, 39), Offset = 0.814 });      //Adding 2nd Gradient Stop

			int ButtonWidth = 170, ButtonHeight = 80;																					//Button Width & Height Variable
			SolidColorBrush ForeBorder = new SolidColorBrush(Colors.Black);																//Button Foreground & BorderBrush Color
			Style ButtonStyle = (Style)Application.Current.Resources["Categories"];														//Button Style
			Thickness ButtonMargin = new Thickness(0, -40, 0, 7);																		//Button Margin

			//Loop For Initializing Each Category Of Thee 15 Categories
			for (int i = 0; i < NumberOfCategories; i++)
            {
				int CatNumber = i + 1;																									//Category Number, Event Parameter
                CategoryButtons.Add(new Button());																						//Adding New Category Button The Category Buttons List
                CategoryButtons[i].Name = NamesAndContents[i];																			//Assigning Button Name
                CategoryButtons[i].Width = ButtonWidth;																					//Giving It Its Width
				CategoryButtons[i].Height = ButtonHeight;																				//Giving It Its Height
                CategoryButtons[i].Content = NamesAndContents[i];																		//Assigning The Content Of It
				CategoryButtons[i].BorderBrush = ForeBorder;																			//Assigning Its BorderBrush Color
				CategoryButtons[i].Foreground = ForeBorder;																				//Assigning Its Foreground Color
                CategoryButtons[i].Background = CategoryButtonColor;																	//Assigning Its Background With The Gradient Coloring System
				CategoryButtons[i].Style = ButtonStyle;																					//Assigning Its Style
				CategoryButtons[i].Margin = ButtonMargin;																				//Assigning Its Margin
				CategoryButtons[i].Click += ((sender, e) => LoadMemeButton(sender, e, CatNumber));										//Giving It An Event Handler With Category Number As Parameter
				ToolsPanel.Children.Add(CategoryButtons[i]);																			//Giving The 1st Tools Panel A Children(Button)
            }
        }

		//Memes Buttons Initializing ForWith Default Arguements For Loading First Category, Called To Handle Category Clicks
		private void LoadMemeButton(object sender = null, EventArgs e = null, int CategoryNo = 1)
        {
			//Loading Buttons For Memes
			List<ImageBrush> MemesButtonsImages = new List<ImageBrush>();																//Memes Buttons Image File
			List<BitmapImage> MemesButtonsBitImages = new List<BitmapImage>();															//Memes Buttons Image Drawn Bitmaps
			List<Button> MemesButtons = new List<Button>();																				//Memes Button

			////Constants & Variables Initializations
			int[] MemesNumber = new int[15] { 10, 6, 4, 2, 3, 20, 6, 16, 11, 6, 9, 3, 5, 15, 3 };										//Index 0 Have Category Nubmers Of Memes Of Category Number One
			int NoOfMemes = MemesNumber[CategoryNo-1];																					//Number Of Memes In The Passed Category Number

			int MemeButtonWidth = 120, MemeButtonHeight = 103;																			//Meme Button Width & Height Variable
			SolidColorBrush MemeButtonBorder = new SolidColorBrush(Colors.Transparent);													//Meme Button BorderBrush Color
			Style MemeButtonStyle = (Style)Application.Current.Resources["MemeStyle"];													//Meme Button Style

			//Clearing The Memes Panel For Replacing With The New Category Memes
			SecondaryToolsPanel.Children.Clear();

            //Loop For Initializing Each Memes Of The (NoOfMemes) Of The Choosen Category
			for (int i = 0; i < NoOfMemes; i++)
            {
				int ImageNo = i + 1;																									//Image Number, Used As Arguement To Event Handler & URI Address

				//Adding A New Bitmap Image To The Memes Buttons Bitmap Images List With The Category Number & The Image Number
				MemesButtonsBitImages.Add(new BitmapImage(new Uri("Assets/Memes/" + CategoryNo + '/' + ImageNo + ".png", UriKind.Relative)));
				//Adding A New Image Brush To The Memes Buttons Image List
				MemesButtonsImages.Add(new ImageBrush());
				//Adding New Button To The Memes Buttons List
				MemesButtons.Add(new Button());
			
                MemesButtonsImages[i].ImageSource = MemesButtonsBitImages[i];															//Assigning The Image Brush For Buttons With The Bitmap Image
				MemesButtons[i].Width = MemeButtonWidth;																				//Giving The Button It Its Width
				MemesButtons[i].Height = MemeButtonHeight;																				//Giving It Its Height
				MemesButtons[i].BorderBrush = MemeButtonBorder;																			//Assigning Its BorderBrush Color
				MemesButtons[i].Foreground = MemesButtonsImages[i];																		//Assigning Its Foreground(Image) With The Image Brush
				MemesButtons[i].Background = MemesButtonsImages[i];																		//Assigning Its Background(Image) With The Image Brush
				MemesButtons[i].Style = MemeButtonStyle;																				//Assigning Its Style
				MemesButtons[i].Click += ((sender2, e2) => Meme_Click(sender, e, CategoryNo, ImageNo));									//Giving It An Event Handler With Image Number As Parameter
				SecondaryToolsPanel.Children.Add(MemesButtons[i]);																		//Giving The 2nd Tools Panel A Children(Meme Button)
            }
            SecondaryToolsScroll.ScrollToHorizontalOffset(0);																			//Move The Scroll Bar To The Beginning
        }

        //Global Constants & Variables Initializations
        List<Image> MemesImages = new List<Image>();																					//List Of Memes Images Files
        List<BitmapImage> MemesBitmapImages = new List<BitmapImage>();																	//List Of Memes Images Drawn Bitmaps
        static int MemeNumbers = 0;																										//Number Of Memes On The Image Screen!
        
		//Loading Memes For Displaying On The Image Grid, With 2 Arguements, Category Number & Meme Number
        private void Meme_Click(object sender, EventArgs e, int Category, int Meme)
        {
			//Adding A New Bitmap Image To The Memes Bitmap Images List With The Category Number & The Image Number    
			MemesBitmapImages.Add(new BitmapImage(new Uri("Assets/Memes/" + Category + '/' + Meme + ".png", UriKind.Relative)));
			MemesImages.Add(new Image());																								//Adding The Bitmap Image To The Memes Images List
			MemesImages[MemeNumbers].Source = MemesBitmapImages[MemeNumbers];															//Assigning The Bitmap Image To The Image
            MemesImages[MemeNumbers].Width = 120;																						//Giving The Meme A Width
            MemesImages[MemeNumbers].Height = 110;																						//Giving The Meme A Height
            ImageCanvas.Children.Add(MemesImages[MemeNumbers]);																			//Adding The Image To The Image Grid As A Children
            MemeNumbers++;																												//Incriminating The Memes Images Number In The Image Grid
        }

        private void OnExit()
        {
            // TODO: Add event handler implementation here.
        }

        private void OnBack()
        {
            // TODO: Add event handler implementation here.
        }

        private void OpenClicked(object sender, EventArgs e)
        {
			PhotoChooserTask ImageFile;
			ImageFile = new PhotoChooserTask();
			ImageFile.Completed += new EventHandler<PhotoResult>(ChooseImage);
			ImageFile.Show();
		}

		private void ChooseImage(object sender, PhotoResult e)
		{
			BitmapImage bmp = new System.Windows.Media.Imaging.BitmapImage();
			bmp.SetSource(e.ChosenPhoto);
			Image test = new Image();
			test.Source = bmp;
			//test.Margin = ImageCanvas.Margin;
			//test.Height = ImageCanvas.ActualHeight;
			//test.Width = ImageCanvas.ActualWidth;
			//MessageBox.Show("Height: " + ImageCanvas.ActualHeight + "Width: " + ImageCanvas.ActualWidth);
			//MessageBox.Show("Height: " + test.Height + "Width: " + test.Width);
			//MessageBox.Show("Height: " + test.ActualHeight + "Width: " + test.ActualWidth);
			SecondaryToolsScroll.Height = 101;
			ImageCanvas.Children.Add(test);
		}

        private void SnapshotClicked(object sender, EventArgs e)
        {
        	// TODO: Add event handler implementation here.
        }

        private void ShareClicked(object sender, EventArgs e)
        {
            // TODO: Add event handler implementation here.
        }

        private void SaveClicked(object sender, EventArgs e)
        {
        	// TODO: Add event handler implementation here.
        }

		private void RateClicked(object sender, EventArgs e)
		{
			// TODO: Add event handler implementation here.
		}
    }
}