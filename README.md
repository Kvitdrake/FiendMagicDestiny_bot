# Telegram Bot in C#
Hi everyone, this is a lazy project of my telegram bot. 

All branches were made for backup and testing of dubious ideas. I realise that it may look crooked, but I was just recording the process of bot creation, not making a nice project "for show". 

In the main thread is my final working version.  

Bot functionality:
- sending messages, reactions to them (base, nothing special)
- converting a string entered by the user into an array of numbers, its further processing (correlation with objects, etc.) 
- creating files, writing data there, formatting the text and sending the finished docx file to the user (and further deleting it from the computer, so as not to waste memory {the customer refused to store files in the database}).
- Works only for the customer and me (which is logical).
 Among the features - random access to the user.  

The bot is still being redesigned due to improved architecture, compliance with SOLID and other programming principles, as well as to increase performance and save memory.  
Although I wrote above that the whole project was not for display, but at the last moment I decided to clean up all the unique data of the programme, as well as specific descriptions and texts by virtue of preserving the customer's copyrights. 

#3 You can test it by downloading the project or by following the link (although you will be able to see only one function, but what can you do) - @FiendMagicDestiny_bot
