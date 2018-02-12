How to use the Dialogue system in Shattered Song

You can manipulate events inside the sequence however you like. That includes loading character portraits, sending text to the text box, 
changing the background, and playing audio.

You will save your dialogue script as a .txt file and place it in the Assets/TextFiles folder with a unique name that makes sense. A programmer 
or other developer will be able to call it at an appropriate time so the dialogue sequence then happens. It will start with whatever command you place
first in the file. 

Most commands are followed with other information, but it all is formatted as follows:

##Command,value,value,value

There may be as few as 1 or 0 values, or several, but if there ever are there is always a comma and no space separating them.

All commands The first line in your file will likely look like the following:

##Background,add,DungeonBackground

Any line with a ## command in front of it means the rest of the line is information the program will use to grab game files. 
##Background means you're doing something to the background of the scene. The next value indicates what it is.
For now, the only options for background are "add" and "remove". The last item, DungeonBackground is the file name of the image used for the background
from the folder "Assets/Resources/BackgroundImages".
So in this case, we are adding the file "DungeonBackground" to the scene and loading it as the background!

The next thing you would probably want to do is load a character portrait. A three examples of what that would look like are the following: 

##Portrait,add,George,unhappy,3
##Portrait,remove,3
##Portrait,move,5,2

The first command takes the "unhappy" image from the "Assets/Resources/Characters/George" file and adds it to position 3 in the dialogue window.
This command doubles as an overwrite as well. If you have a portrait of a character with one expression and want to replace it with a different expression
(or a different character entirely, it doesn't matter) you can just ##Portrait,add and it will replace what is already there.

More than one filetype works, but in order to have the best quality, use a .PNG image. This allows you to keep the part of the picture surrounding your character
transparent, so it won't show up in the scene.

There are 8 "positions" in the dialogue window. They range from 1 at the far left, to 4 in the center, to 7 at the far right. 
8 is also in the center, and i'll get back to that later. All character portraits should be facing from left to right. 
If they are ever placed in positon 5, 6, 7, or 8, they will automatically be flipped and therefore be facing from right to left.
So the first ##Portrait command takes the unhappy portrait of George and puts it in position 3.
According to the above, this means it's just left of center and facing towards the right.

The second ##Portrait command is a little bit easier to explain. It simply takes the image in position 3 and makes whatever is in it invisible.
This effectively removes it from the scene.

The ##Portrait,move command takes the image found in the first position specified and places it in the second position specified.
There isn't much more to it.

There are also commands for Speech Bubbles, Foreground Images and Audio.

The speech bubble is changed in the following formats:

##Box,add,filename

where the file "filename" needs to be located in "Assets/Resources/SpeechBoxes"

the ##Foreground command is used exactly the same as the ##Background command, except the files are saved in "Assets/Resources/ForegroundImages" instead of 
"Assets/Resources/BackgroundImages". You are, however, more likely to want to remove a foreground image, so the ##Foreground,remove command becomes more appealing to use.

Audio is slightly more complicated but really simple. The command changes slightly based on whether you are planning on using sound that a character is making or 
if it's an environmental sound or sound effect.

For speech, use the following:

##Audio,Speech,CharacterName,filename

I'm not certain the extent of audio filetypes that Unity works with, but it certainly works with .MP3 files.

For other audio sources, organize the audio like so:

##Audio,Other,filename

Both audio commands play the audio immediately.

Please make sure to test the functionality and give me feedback! It should be pretty easy to add additional functionality, so let me know!

- Bryce Collins, CSUS Game Development Club
February 12th, 2018

