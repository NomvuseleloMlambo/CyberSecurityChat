using System;
using System.Collections.Generic;
using System.Media;
using System.Threading;

namespace CyberSecurityChat
{
    class Program
    {
        static string lastTopic = "";
        static Dictionary<string, string> topicHistory = new Dictionary<string, string>();
        static List<string> phishingTips = new List<string>
        {
            "Be cautious of emails asking for personal information.",
    "Scammers often disguise themselves as trusted organizations.",
    "Never click on suspicious links in emails or messages.",
    "Always verify the sender's email address.",
    "Look for grammar mistakes in suspicious emails."
        };

        static void Main(string[] args)
        {
            // Play the greeting at startup
            PlayGreeting();

            // Display ASCII art logo
            ShowAsciiArt();

            Thread.Sleep(11000);

            // Greet user and ask for name
            string userName = GreetUser();

            // Start chatbot
            StartChatbot(userName);

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        static void PlayGreeting()
        {
            try
            {
                SoundPlayer player = new SoundPlayer("Resources/greeting.wav");
                player.Load();
                player.Play();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Audio error: " + ex.Message);
            }
        }

        static void ShowAsciiArt()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(@"     
                     ________
                    / _______\ 
                   | |      | |  
                   | | LOCK | |   
                   | |______| | 
                    \________/    

                  [CYBER BOT]
               Cybersecurity Awareness
");
            Console.ResetColor();
        }

        static string GreetUser()
        {
            Console.WriteLine("\nWhat's your name?");
            string name = Console.ReadLine();

            while (string.IsNullOrWhiteSpace(name))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Please enter a valid name.");
                Console.ResetColor();
                name = Console.ReadLine();
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\nWelcome, {name}! I'm your Cybersecurity Awareness Assistant.");
            Console.WriteLine("Ask me anything about staying safe online!");
            Console.ResetColor();

            return name;
        }

        static void StartChatbot(string userName)
        {
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                TypeResponse("\nWhat would you like to ask about cybersecurity? (Type 'exit' to quit)");
                Console.ResetColor();

                string userInput = Console.ReadLine()?.Trim();

                // Handle empty or whitespace input
                if (string.IsNullOrWhiteSpace(userInput))
                {
                    TypeResponse("\nI didn’t catch that. Please type your question again.");
                    continue;
                }

                // Handle exit command
                if (userInput.ToLower() == "exit")
                {
                    Console.WriteLine($"\nGoodbye, {userName}! Stay safe online. 👋");
                    break;
                }

                GetResponse(userInput);
            }
        }

        static void GetResponse(string userInput)
        {
            userInput = userInput.ToLower();

            // Step 1: Sentiment Detection
            string sentiment = DetectSentiment(userInput);

            if (sentiment == "concern")
            {
                TypeResponse("\nIt's completely understandable to feel that way. Cyber threats can be stressful, but I'm here to help you feel more secure.");
            }
            else if (sentiment == "curiosity")
            {
                TypeResponse("\nCuriosity is great! The more you learn about cybersecurity, the better prepared you'll be.");
            }
            else if (sentiment == "frustration")
            {
                TypeResponse("\nI get that this can be frustrating. Let’s take it one step at a time — I’m here to make it easier.");
            }

            // Step 2: Memory and Follow-Up Check
            if (userInput.Contains("remind") || userInput.Contains("what did you say") || userInput.Contains("repeat") || userInput.Contains("again"))
            {
                if (!string.IsNullOrEmpty(lastTopic) && topicHistory.ContainsKey(lastTopic))
                {
                    TypeResponse($"\nSure! Here's what I said about {lastTopic}:");
                    TypeResponse(topicHistory[lastTopic]);
                }
                else
                {
                    TypeResponse("\nI can't recall a topic yet. Try asking me something first.");
                }
                return;
            }

            if (userInput.Contains("how are you"))
            {
                TypeResponse("\nI'm doing great, thank you! I'm always ready to help you stay safe online.");
            }
            else if (userInput.Contains("what's your purpose") || userInput.Contains("what is your purpose"))
            {
                TypeResponse("\nMy purpose is to educate and assist you with cybersecurity knowledge so you can protect yourself online.");
            }
            else if (userInput.Contains("what can i ask") || userInput.Contains("topics"))
            {
                TypeResponse("\nYou can ask about:");
                TypeResponse("- Phishing");
                TypeResponse("- Strong passwords");
                TypeResponse("- Malware");
                TypeResponse("- Safe browsing");
                TypeResponse("- Common scams");
                TypeResponse("- Social engineering");
            }
            if (userInput.Contains("remind") || userInput.Contains("what did you say") || userInput.Contains("repeat") || userInput.Contains("again"))
            {
                if (!string.IsNullOrEmpty(lastTopic) && topicHistory.ContainsKey(lastTopic))
                {
                    TypeResponse($"\nSure! Here's what I said about {lastTopic}:");
                    TypeResponse(topicHistory[lastTopic]);
                }
                else
                {
                    TypeResponse("\nI can't recall a topic yet. Try asking me something first.");
                }
                return;
            }

            if (lastTopic == "phishing" && (userInput.Contains("more") || userInput.Contains("another tip")))
            {
                Random rand = new Random();
                string newTip = phishingTips[rand.Next(phishingTips.Count)];
                TypeResponse("\nHere's another phishing tip: " + newTip);
                topicHistory["phishing"] = newTip;
            }
            else if (userInput.Contains("phishing"))
            {
                lastTopic = "phishing";
                Random rand = new Random();
                string tip = phishingTips[rand.Next(phishingTips.Count)];
                TypeResponse("\n" + tip);
                topicHistory["phishing"] = tip;
            }

            else if (userInput.Contains("strong password") || userInput.Contains("create password") || userInput.Contains("password"))
            {
                lastTopic = "password";
                string msg = "\nMake sure to use strong, unique passwords for each account. Avoid using personal info like your birthday.";
                TypeResponse(msg);
                topicHistory["password"] = msg;
            }
            else if (userInput.Contains("malware"))
            {
                lastTopic = "malware";
                string msg = "\nMalware is malicious software like viruses, spyware, or ransomware that can damage or steal your data.";
                TypeResponse(msg);
                topicHistory["malware"] = msg;
            }
            else if (userInput.Contains("safe browsing"))
            {
                lastTopic = "safe browsing";
                string msg = "\nSafe browsing means avoiding unknown websites, not clicking suspicious links, and using secure connections.";
                TypeResponse(msg);
                topicHistory["safe browsing"] = msg;
            }
            else if (userInput.Contains("social engineering"))
            {
                lastTopic = "social engineering";
                string msg = "\nSocial engineering is when someone tricks you into giving away confidential information by pretending to be someone you trust.";
                TypeResponse(msg);
                topicHistory["social engineering"] = msg;
            }
            else if (userInput.Contains("scam"))
            {
                lastTopic = "scam";
                string msg = "\nWatch out for online scams. Never click on suspicious links or give out personal information to unverified sources.";
                TypeResponse(msg);
                topicHistory["scam"] = msg;
            }
            else if (userInput.Contains("privacy"))
            {
                lastTopic = "privacy";
                string msg = "\nProtect your privacy by adjusting security settings on social media and apps. Only share personal information when absolutely necessary.";
                TypeResponse(msg);
                topicHistory["privacy"] = msg;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                TypeResponse("\nI'm not sure I understand. Can you try rephrasing or ask about a cybersecurity topic like phishing or malware?");
                Console.ResetColor();
            }
        }
        static string DetectSentiment(string input)
        {
            input = input.ToLower();

            if (input.Contains("worried") || input.Contains("scared") || input.Contains("nervous") || input.Contains("anxious") || input.Contains("concern") || input.Contains("concerned"))
                return "concern";
            else if (input.Contains("curious") || input.Contains("interested") || input.Contains("wondering"))
                return "curiosity";
            else if (input.Contains("frustrated") || input.Contains("annoyed") || input.Contains("confused") || input.Contains("overwhelmed"))
                return "frustration";
            else
                return "neutral";
        }
        static void TypeResponse(string message, int delay = 25)
        {
            foreach (char c in message)
            {
                Console.Write(c);
                Thread.Sleep(delay);
            }
            Console.WriteLine();
        }
    }
}
