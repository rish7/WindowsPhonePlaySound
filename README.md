WindowsPhonePlaySound
=====================

Play Spund Using SoundEffect and Media Element in Windows Phone


Using "Media Element" :

MediaElement mediaEle = new MediaElement();
mediaEle.AutoPlay = true;
mediaEle.Source = new Uri("sounds/song1.wav", UriKind.Relative);
mediaEle.Volume = 1f;
mediaEle.Visibility = Visibility.Collapsed;
mediaEle.Play();

Using "Sound Effect" :

SoundEffect soundOn;

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

LoadSoundFile("sounds/on.wav", out soundOn);

soundOn.Play();