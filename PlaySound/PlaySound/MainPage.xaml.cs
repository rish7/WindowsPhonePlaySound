using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using System.Windows.Navigation;
using System.Windows.Resources;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;


namespace PlaySound
{
    public partial class MainPage : PhoneApplicationPage
    {
        // The sounds to play
        private SoundEffect soundOn, soundOff, soundLogon;

        // Flag that indicates if we need to resume Zune playback upon exiting.
        bool resumeMediaPlayerAfterDone = false;

        // Constructor
        public MainPage()
        {
            InitializeComponent();

            /**********
             // Timer to simulate the XNA game loop (SoundEffect class is from the XNA Framework)
            GameTimer gameTimer = new GameTimer();
            gameTimer.UpdateInterval = TimeSpan.FromMilliseconds(33);

            // Call FrameworkDispatcher.Update to update the XNA Framework internals.
            gameTimer.Update += delegate { try { FrameworkDispatcher.Update(); } catch { } };

            // Start the GameTimer running.
            gameTimer.Start();
             
            ***********/


            // Prime the pump or we'll get an exception.
            FrameworkDispatcher.Update();  //We have Update FrameworkDispatcher to play sound using SoundEffect

            LoadSoundFile("sounds/logon.wav", out soundLogon);
            LoadSoundFile("sounds/on.wav", out soundOn);
            LoadSoundFile("sounds/off.wav", out soundOff);

            soundLogon.Play();

            mediaEle.Source = new Uri("sounds/Sleep Away.mp3", UriKind.Relative);
            mediaEle.Volume = 1f;
            mediaEle.MediaEnded += new RoutedEventHandler(mediaEle_MediaEnded);

            mediaEle.Visibility = Visibility.Collapsed;
        }

        private void mediaEle_MediaEnded(object sender, RoutedEventArgs e)
        {
            if (chkRepeatbgSound.IsChecked==true)
            {
                // Loop the ambience sound.
                mediaEle.Position = TimeSpan.Zero;
                mediaEle.Play();
            }
        }

        private void LoadSoundFile(String FilePath, out SoundEffect Sound)
        {            
            Sound = null;
            try
            {
                // Holds informations about a file stream.
                StreamResourceInfo SoundFileInfo = App.GetResourceStream(new Uri(FilePath, UriKind.Relative));
                // Create the SoundEffect from the Stream
                Sound = SoundEffect.FromStream(SoundFileInfo.Stream);
            }
            catch (NullReferenceException)
            {
                // Display an error message
                MessageBox.Show("unable to load sound file from this path :" + FilePath);
            }
        }

        private void btnSoundOn_Click(object sender, RoutedEventArgs e)
        {
            //Play Sound on            
            try
            {
                soundOn.Play();
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Can't play, Sound on is null.");
            }
        }

        private void btnSoundOff_Click(object sender, RoutedEventArgs e)
        {
            //Play Sound off
            soundOff.Play();
        }

        private void btnPlayBgSound_Click(object sender, RoutedEventArgs e)
        {
            //Play bg sound
            mediaEle.Play();
        }

        private void btnPauseBgSound_Click(object sender, RoutedEventArgs e)
        {
            //Pause bg Sound
            mediaEle.Pause();
        }

        private void btnStopBgSound_Click(object sender, RoutedEventArgs e)
        {
            //Stop bg Sound
            mediaEle.Stop();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // If the MediaPlayer is already playing music, pause it upon entering our app.
            ZunePause();
        }


        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            // If the MediaPlayer was already playing music, resume playback as we leave our app.
            ZuneResume();
        }


        #region Zune Pause/Resume

        private void ZunePause()
        {
            // Please see the MainPage() constructor above where the GameTimer object is created.
            // This enables the use of the XNA framework MediaPlayer class by pumping the XNA FrameworkDispatcher.

            // Pause the Zune player if it is already playing music.
            if (!MediaPlayer.GameHasControl)
            {
                MediaPlayer.Pause();
                resumeMediaPlayerAfterDone = true;
            }
        }

        private void ZuneResume()
        {
            // If Zune was playing music, resume playback
            if (resumeMediaPlayerAfterDone)
            {
                MediaPlayer.Resume();
            }
        }

        #endregion Zune Pause/Resume

    }
}