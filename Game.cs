using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

using SFML;
using SFML.Audio;
using SFML.Graphics;
using SFML.Window;
using System.IO;

namespace ld28_2
{
	class Achivement
	{
		public string Text { get; set; }
		public bool Done { get; set; }
	}

	class Game
	{
		ContextSettings settings;
		Level level;
		Player player;
		int levelID = 0;
		bool up, down, left, right;
		Dictionary<int, Achivement> achivements;

		bool focus = true;

		RenderTexture light;
		CircleShape lightCircle;

		Shader blur;

		bool playing = false;

		bool highQuality;

		public static int Width, Height;

		string ln1;
		string ln2;
		string ln3;

		bool drawCliparts = false;
		Texture[] cliparts;
		int clipart = 0;

		int recentAttack = 0;

		bool resumable = false;
		bool aiming = false;
		bool suiciding = false;

		bool achivement = false;
		string achivementtext = "";

		ParticleSystem bloodSystem;

		public Game(uint antialias, bool hq)
		{
			settings = new ContextSettings();
			settings.AntialiasingLevel = antialias;
			player = new Player();
			level = new Level();
			highQuality = hq;
		}

		public void ResumeGame()
		{
			Achivement(0);
			playing = true;
			if (!resumable)
			{
				levelID = 0;
				Finish();
				player.Armed = false;
				player.canShoot = false;
				player.Health = 1000;
				suiciding = false;
			}
		}

		public void Finish()
		{
			if (levelID == 0)
			{
				level.Load("Content/lvl1.txt");
				levelID++;
			}
			else if (levelID == 1)
			{
				level.Load("Content/lvl2.txt");
				levelID++;
			}
			else if (levelID == 2)
			{
				level.Load("Content/lvl3.txt");
				levelID++;
			}
			else if (levelID == 3)
			{
				player.Arm();
				level.Load("Content/lvl4.txt");
				level.Office = true;
				levelID++;
			}
			else if (levelID == 4)
			{
				cliparts = new Texture[4];
				cliparts[0] = new Texture("Content/clipart0.png");
				cliparts[1] = new Texture("Content/clipart1.png");
				cliparts[2] = new Texture("Content/clipart2.png");
				MakeSequence();
				levelID++;
			}
			else if (levelID == 5)
			{
				player.canShoot = true;
				drawCliparts = false;
				aiming = true;
				levelID++;
			}
			else if (levelID == 6)
			{
				aiming = false;
				player.canShoot = false;
				level.Load("Content/lvl5.txt");
				WriteLn("Whoa what happend?");
				new Thread(() => { Thread.Sleep(2000); WriteLn(""); WriteLn("WHERE IS HE? I WANT TO KILL HIM!"); Thread.Sleep(5000); WriteLn(""); WriteLn(""); WriteLn("he escaped..."); Thread.Sleep(5000); WriteLn("IM SUCH A TERRIBLE SHOOTER"); Thread.Sleep(2000); WriteLn("I'll commit suicide now..."); suiciding = true; }).Start();
				levelID++;
			}

			player.Location = level.Start * 32 + 16;
		}

		public void MakeSequence()
		{
			drawCliparts = true;
			clipart = 0;
			WriteLn("");
			WriteLn("");
			WriteLn("");
			WriteLn("You shouldn't have done that!");
			new Thread(() => { Thread.Sleep(2000); clipart = 1; WriteLn(""); WriteLn(""); WriteLn("What? No! It was just a mistake. Believe me"); Thread.Sleep(5000); WriteLn("Please let me continue working on my Game for Ludum Dare 28"); Thread.Sleep(5000); WriteLn(""); WriteLn(""); WriteLn("N"); Thread.Sleep(200); ln1 += "o"; Thread.Sleep(200); ln1 += "."; Thread.Sleep(200); ln1 += "."; Thread.Sleep(200); ln1 += "!"; Thread.Sleep(1000); clipart = 2; WriteLn(""); WriteLn("Say Goodbye!"); Thread.Sleep(1500); Finish(); }).Start();
		}

		public void WriteLn(string ln)
		{
			ln3 = ln2;
			ln2 = ln1;
			ln1 = ln;
		}

		public bool Shoot(Location l, float rot)
		{
			return false;
		}

		public void Dead()
		{
			ShowTitlescreen(false);
		}

		public void Lighten(Location l, float rad, float strength = 1)
		{
			if (highQuality)
			{
				Color color = Color.White;
				float opacity;
				for (int i = 10; i >= 0; i -= 1)
				{
					opacity = 1 / (float)Math.Max(1, i * 2);
					float radi = rad * 0.1f * i;
					lightCircle.Radius = radi;
					lightCircle.Origin = new Vector2f(radi, radi);
					lightCircle.Position = l.Vec2f;
					lightCircle.FillColor = new Color(color.R, color.G, color.B, (byte)(opacity * 255 * strength));
					light.Draw(lightCircle);
				}
			}
			else
			{
				Color color = Color.White;
				color.A = (byte)(strength * color.A);
				lightCircle.Radius = rad * 0.6f;
				lightCircle.Origin = new Vector2f(rad * 0.6f, rad * 0.6f);
				lightCircle.Position = l.Vec2f;
				lightCircle.FillColor = color;
				light.Draw(lightCircle);
			}
		}

		public void ShowTitlescreen(bool resumable)
		{
			if (suiciding)
			{
				Achivement(2);
			}
			playing = false;
			this.resumable = resumable;
		}

		public void SaveAchivements()
		{
			File.WriteAllText("ach.txt", "0:" + (achivements[0].Done ? "1" : "0") + "\n1:" + (achivements[1].Done ? "1" : "0") + "\n2:" + (achivements[2].Done ? "1" : "0") + "");
		}

		public void Achivement(int id)
		{
			if (!achivements[id].Done)
			{
				achivement = true;
				achivementtext = achivements[id].Text;
				achivements[id].Done = true;
				SaveAchivements();
				new Thread(() => { Thread.Sleep(5000); achivement = false; }).Start();
			}
		}

		private int lastTick;
		private int lastFrameRate;
		private int frameRate;

		public int CalculateFrameRate()
		{
			if (System.Environment.TickCount - lastTick >= 1000)
			{
				lastFrameRate = frameRate;
				frameRate = 0;
				lastTick = System.Environment.TickCount;
			}
			frameRate++;
			return lastFrameRate;
		}

		public void Start()
		{
			System.Drawing.Rectangle r = System.Windows.Forms.Screen.PrimaryScreen.Bounds;
			RenderWindow window = new RenderWindow(new VideoMode(1024, 786), "One Bullet", Styles.Titlebar | Styles.Close, settings);
			window.LostFocus += (s, e) => { focus = false; };
			window.GainedFocus += (s, e) => { focus = true; };
			Width = (int)window.Size.X;
			Height = (int)window.Size.Y;
			window.Closed += window_Closed;

			window.KeyPressed += window_KeyPressed;
			window.KeyReleased += window_KeyReleased;

			Finish();

			player.OnShoot += Shoot;

			window.SetFramerateLimit(60);

			Font font = new Font("Content/consolas.ttf");

			Text healthText = new Text("", font, 72);
			healthText.Color = Color.Red;

			Text dialog = new Text("", font, 48);
			dialog.Color = new Color(128, 128, 128);

			Texture bulletUI = new Texture("Content/bulletUI.png");

			RectangleShape bulletRect = new RectangleShape();
			bulletRect.Size = new Vector2f(72, 108);
			bulletRect.Origin = new Vector2f(72, 108);
			bulletRect.Texture = bulletUI;

			light = new RenderTexture(window.Size.X, window.Size.Y);

			lightCircle = new CircleShape(0, 20);

			RectangleShape lightDisplay = new RectangleShape();
			lightDisplay.Size = new Vector2f(window.Size.X, window.Size.Y);
			lightDisplay.Position = new Vector2f();

			blur = new Shader("Content/blur.vs", "Content/blur.fs");
			blur.SetParameter("rad", 0.004f);

			RenderStates blurStates = new RenderStates(RenderStates.Default);
			blurStates.Shader = blur;

			RectangleShape imageFrame = new RectangleShape();
			imageFrame.Position = new Vector2f(Width / 2 - 400, 16);
			imageFrame.Size = new Vector2f(800, 600);

			Texture white = new Texture(1, 1);
			Texture blood = new Texture("Content/bloodOverlay.png");

			Text startBtn = new Text("START GAME", font, 100);
			startBtn.Position = new Vector2f(Width / 2 - startBtn.GetGlobalBounds().Width / 2, Height / 5);

			bloodSystem = new ParticleSystem(new Texture("Content/bloodParticle.png"));

			Texture aimImage = new Texture("Content/finalScene.png");

			Text copyright = new Text("Game by WebFreak001. Made for Ludum Dare 28", font, 48);
			copyright.Position = new Vector2f(Width / 2 - copyright.GetLocalBounds().Width / 2, 32);

			bool mouseOld = false;

			#region Achivements

			if (!File.Exists("ach.txt"))
			{
				File.WriteAllText("ach.txt", "0:0\n1:0\n2:0");
			}

			achivements = new Dictionary<int, Achivement>();
			string[] achs = File.ReadAllLines("ach.txt");
			bool done1 = false;
			bool done2 = false;
			bool done3 = false;
			if (achs.Length > 2)
			{
				if (achs[0].StartsWith("0"))
				{
					char c = achs[0].Last();
					if (c == '1') done1 = true;
				}
				if (achs[1].StartsWith("1"))
				{
					char c = achs[1].Last();
					if (c == '1') done2 = true;
				}
				if (achs[2].StartsWith("1"))
				{
					char c = achs[2].Last();
					if (c == '1') done3 = true;
				}
			}
			achivements.Add(0, new Achivement() { Done = done1, Text = "Start the game" });
			achivements.Add(1, new Achivement() { Done = done2, Text = "You Missed" });
			achivements.Add(2, new Achivement() { Done = done3, Text = "Finish the Game" });

			Texture achiveTex = new Texture("Content/achivement.png");
			achiveTex.Smooth = true;
			RectangleShape achTL = new RectangleShape();
			RectangleShape achT = new RectangleShape();
			RectangleShape achTR = new RectangleShape();
			RectangleShape achL = new RectangleShape();
			RectangleShape achR = new RectangleShape();
			RectangleShape achBL = new RectangleShape();
			RectangleShape achB = new RectangleShape();
			RectangleShape achBR = new RectangleShape();
			RectangleShape ach = new RectangleShape();

			achTL.TextureRect = new IntRect(0, 0, 32, 32);
			achT.TextureRect = new IntRect(31, 0, 2, 32);
			achTR.TextureRect = new IntRect(32, 0, 32, 32);
			achL.TextureRect = new IntRect(0, 31, 32, 2);
			achR.TextureRect = new IntRect(32, 31, 32, 2);
			achBL.TextureRect = new IntRect(0, 32, 32, 32);
			achB.TextureRect = new IntRect(31, 32, 2, 32);
			achBR.TextureRect = new IntRect(32, 32, 32, 32);
			ach.TextureRect = new IntRect(31, 31, 2, 2);

			achTL.Position = new Vector2f(Width / 2 - 232, 16);
			achT.Position = new Vector2f(Width / 2 - 200, 16);
			achTR.Position = new Vector2f(Width / 2 + 200, 16);
			achL.Position = new Vector2f(Width / 2 - 232, 48);
			achR.Position = new Vector2f(Width / 2 + 200, 48);
			achBL.Position = new Vector2f(Width / 2 - 232, 118);
			achB.Position = new Vector2f(Width / 2 - 200, 118);
			achBR.Position = new Vector2f(Width / 2 + 200, 118);
			ach.Position = new Vector2f(Width / 2 - 200, 48);

			achTL.Size = new Vector2f(32, 32);
			achT.Size = new Vector2f(400, 32);
			achTR.Size = new Vector2f(32, 32);
			achL.Size = new Vector2f(32, 70);
			achR.Size = new Vector2f(32, 70);
			achBL.Size = new Vector2f(32, 32);
			achB.Size = new Vector2f(400, 32);
			achBR.Size = new Vector2f(32, 32);
			ach.Size = new Vector2f(400, 70);

			achTL.Texture = achiveTex;
			achT.Texture = achiveTex;
			achTR.Texture = achiveTex;
			achL.Texture = achiveTex;
			achR.Texture = achiveTex;
			achBL.Texture = achiveTex;
			achB.Texture = achiveTex;
			achBR.Texture = achiveTex;
			ach.Texture = achiveTex;

			Text achivementGetText = new Text("Achivement Get!", font, 40);
			achivementGetText.Position = new Vector2f(Width / 2 - 200, 32);
			achivementGetText.Color = Color.Black;
			Text achivementContent = new Text("", font, 26);
			achivementContent.Position = new Vector2f(Width / 2 - 200, 80);
			achivementContent.Color = Color.Black;

			Text fps = new Text("", font, 20);
			fps.Position = new Vector2f(16, 16);

			#endregion



			while (window.IsOpen())
			{
				window.DispatchEvents();

				light.Clear(Color.Transparent);
				window.Clear(Color.Black);

				if (playing)
				{
					if (aiming)
					{
						imageFrame.Texture = aimImage;
						window.Draw(imageFrame);
						if (Mouse.IsButtonPressed(Mouse.Button.Left) && !mouseOld)
						{
							Vector2i m = Mouse.GetPosition(window);
							if (m.X > imageFrame.Position.X && m.Y > imageFrame.Position.Y && m.X < imageFrame.Position.X + imageFrame.Size.X && m.Y < imageFrame.Position.Y + imageFrame.Size.Y)
							{
								Finish();
							}
							else
							{
								Achivement(1);
								Finish();
							}
						}
						mouseOld = Mouse.IsButtonPressed(Mouse.Button.Left);
					}
					else
					{
						if (focus)
						{
							if (up) player.MoveAbsolute(0, -1);
							if (down) player.MoveAbsolute(0, 1);
							if (left) player.MoveAbsolute(-1, 0);
							if (right) player.MoveAbsolute(1, 0);
							int result = level.Move(player.PreUpdate());

							if (result == 1 || result == 2) player.Update();
							else if (result == 3) Finish();
							else if (result == 4)
							{
								WriteLn("I must kill IT");
								WriteLn("I've found a gun... But it has only 1 Bullet.");
								level.RemoveGun();
								player.Arm();
								player.Update();
							}
							else if (result == 5)
							{
								WriteLn("But I have only 1 try. If i don't hit him he will call the police");
								WriteLn("In there is the man who had sex with my wife!");
								WriteLn("That's the Office...");
								player.Update();
							}
							else player.Stuck();

							if (level.Intersect(player) || suiciding)
							{
								player.Health--;
								recentAttack = 10;
								bloodSystem.Emit(player.Location);
								level.ShakeScreen(0.4f, 1);
								if (player.Health <= 0)
								{
									Dead();
								}
							}
							if (Mouse.IsButtonPressed(Mouse.Button.Left) && !mouseOld)
							{
								player.Shoot();
							}
							mouseOld = Mouse.IsButtonPressed(Mouse.Button.Left);
						}
						if (drawCliparts)
						{
							imageFrame.Texture = cliparts[clipart];
							window.Draw(imageFrame);
						}
						else
						{

							player.Rotate(Mouse.GetPosition(window) - level.offset.Vec2i);

							level.Draw(window, player);

							foreach (Location l in level.Lights)
							{
								Lighten(l * 32 + 16 + level.offset, 150.0f);
							}

							bloodSystem.Draw(window, level.offset);

							player.Draw(window, level.offset);

							Lighten(player.Location + level.offset, 100.0f, 0.5f);

							light.Display();

							lightDisplay.Texture = light.Texture;
							lightDisplay.TextureRect = new IntRect(0, 0, (int)light.Size.X, (int)light.Size.Y);
							blurStates.Texture = light.Texture;
							window.Draw(lightDisplay, blurStates);

							if (player.canShoot)
							{
								bulletRect.Position = new Vector2f(window.Size.X - 16, window.Size.Y - 16);
								window.Draw(bulletRect);
							}
						}

						healthText.DisplayedString = (player.Health * 0.1f) + "%";
						healthText.Position = new Vector2f(16, window.Size.Y - healthText.GetGlobalBounds().Height - 48);
						window.Draw(healthText);
						dialog.CharacterSize = 42;
						dialog.DisplayedString = ln1;
						dialog.Position = new Vector2f(window.Size.X / 2 - dialog.GetGlobalBounds().Width / 2, window.Size.Y - 135);
						window.Draw(dialog);
						dialog.CharacterSize = 34;
						dialog.DisplayedString = ln2;
						dialog.Position = new Vector2f(window.Size.X / 2 - dialog.GetGlobalBounds().Width / 2, window.Size.Y - 90);
						window.Draw(dialog);
						dialog.CharacterSize = 26;
						dialog.DisplayedString = ln3;
						dialog.Position = new Vector2f(window.Size.X / 2 - dialog.GetGlobalBounds().Width / 2, window.Size.Y - 50);
						window.Draw(dialog);

						lightDisplay.Texture = blood;
						lightDisplay.TextureRect = new IntRect(0, 0, 800, 600);
						lightDisplay.FillColor = new Color(255, 255, 255, (byte)(recentAttack / 10.0f * 200));
						window.Draw(lightDisplay);

						recentAttack--;
						recentAttack = Math.Max(recentAttack, 0);

						lightDisplay.Texture = white;
						lightDisplay.FillColor = new Color(255, 20, 23, (byte)((1000 - player.Health) / 1000.0f * 50));
						window.Draw(lightDisplay);
						lightDisplay.FillColor = Color.White;
					}
				}
				else
				{
					window.Draw(startBtn);
					if (Mouse.IsButtonPressed(Mouse.Button.Left) && !mouseOld)
					{
						Vector2i p = Mouse.GetPosition(window);
						if (p.Y > startBtn.Position.Y && p.Y < startBtn.Position.Y + 150)
						{
							ResumeGame();
						}
					}

					mouseOld = Mouse.IsButtonPressed(Mouse.Button.Left);
				}
				if (achivement)
				{
					window.Draw(achTL);
					window.Draw(achT);
					window.Draw(achTR);
					window.Draw(achL);
					window.Draw(ach);
					window.Draw(achR);
					window.Draw(achBL);
					window.Draw(achB);
					window.Draw(achBR);
					window.Draw(achivementGetText);
					achivementContent.DisplayedString = achivementtext;
					window.Draw(achivementContent);
				}


				window.Display();
			}
		}

		void window_Closed(object sender, EventArgs e)
		{
			((RenderWindow)sender).Close();
		}

		void window_KeyReleased(object sender, KeyEventArgs e)
		{
			if (e.Code == Keyboard.Key.W)
			{
				up = false;
			}
			else if (e.Code == Keyboard.Key.S)
			{
				down = false;
			}
			if (e.Code == Keyboard.Key.A)
			{
				left = false;
			}
			else if (e.Code == Keyboard.Key.D)
			{
				right = false;
			}
			if (e.Code == Keyboard.Key.Escape)
			{
				if (playing)
				{
					ShowTitlescreen(true);
				}
			}
		}

		void window_KeyPressed(object sender, KeyEventArgs e)
		{
			if (e.Code == Keyboard.Key.W)
			{
				up = true;
			}
			else if (e.Code == Keyboard.Key.S)
			{
				down = true;
			}
			if (e.Code == Keyboard.Key.A)
			{
				left = true;
			}
			else if (e.Code == Keyboard.Key.D)
			{
				right = true;
			}
		}
	}
}
