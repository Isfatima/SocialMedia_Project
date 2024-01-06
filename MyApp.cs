using socialmedia.UserControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using static System.Windows.Forms.LinkLabel;
using System.Xml.Linq;

namespace socialmedia
{
    public partial class MyApp : Form
    {
        private User activeUser;
        public MyApp()
        {
            InitializeComponent();
        }
        public MyApp(string a, string b) : this()
        {
            activeUser = new User(a, b);
        }

        private void addUserControl(UserControl userControl)
        {
            userControl.Dock = DockStyle.Fill;
            ContainerPanel.Controls.Clear();
            ContainerPanel.Controls.Add(userControl);
            userControl.BringToFront();
        }

        private void HomeButton_Click(object sender, EventArgs e)
        {
            HomePanel uc = new HomePanel();
            // imp

            TextBox createPostTextBox = new TextBox();
            createPostTextBox.Size = new Size(400, 20);
            createPostTextBox.Location = new Point(3, 6);
            uc.Controls.Add(createPostTextBox);

            Button createPostButton = new Button();
            createPostButton.Size = new Size(61, 26);
            createPostButton.Location = new Point(408, 3);
            createPostButton.Text = "Post";
            createPostButton.BackColor = Color.White;
            createPostButton.Click += (s, ev) =>
            {
                string postText = createPostTextBox.Text;
                createPostButton.Tag = postText;
                createPostTextBox.Text = "";
            };
            createPostButton.MouseDown += (s, ev) =>
            {
                createPostButton.BackColor = Color.Red;
            };

            createPostButton.MouseUp += (s, ev) =>
            {
                createPostButton.BackColor = SystemColors.Control;
            };
            createPostButton.Click += CreatePostButton_click;
            uc.Controls.Add(createPostButton);

            // show friends posts
            activeUser.GetFriends().Clear();// clear it since i am reading it again from the file
            activeUser.GetPosts().Clear();// clear it since i am reading it again from the file

            bool testpost = false;
            string postpath = "UsersPosts.txt";
            if (File.Exists(postpath))
            {
                string userpath = "UsersInfo.txt";
                string[] lines = File.ReadAllLines(userpath);
                int yOffset1 = 32, yOffset2 = 80, yOffset11 = 50;

                for (int i = 0; i < lines.Length; i++) // Get the list of Friend of the active user
                {
                    string[] userData = lines[i].Split('|');
                    if (userData.Length > 0 && userData[0] == activeUser.GetUsername())
                        if (userData[2] != "")
                        {
                            string[] usernames = userData[2].Split('>');
                            foreach (string usrn in usernames)
                            {
                                User a = new User(usrn);
                                activeUser.GetFriends().Add(a);
                            }
                        }
                }
                
                lines = File.ReadAllLines(postpath);
                for (int i = (lines.Length - 1); i >= 0; i--) // for every line in the post file according to the newest Timestamp
                {
                    string[] postData = lines[i].Split('|');
                    if (postData.Length > 0 && (activeUser.GetFriends().Any(user => user.GetUsername() == postData[0]) || postData[0] == activeUser.GetUsername()) )
                    {
                        if (postData[1] != "")
                        {
                            testpost = true;
                            User a = new User(postData[0]);
                            string[] post_time = postData[1].Split('<'); //post_time[0]=content ,post_time[1]=timestamp
                            Post P = new Post(post_time[0], DateTime.Parse(post_time[1]));
                            P.GetLikes().Clear(); // clear it since i am reading it again from the file
                            if (postData[0] == activeUser.GetUsername()) // 3am 3abe List of Posts lal activeuser ma3 likes w comments
                            {
                                if (postData[2] != "")
                                {
                                    string[] likes = postData[2].Split('>');
                                    foreach (string l in likes)
                                    {
                                        User b = new User(l);
                                        P.GetLikes().Add(b);
                                    } 
                                }
                                if (postData[3] != "") // for now we don't use it cz we are assigning the same event handler in the profile and in the home button when showing posts
                                {
                                    string[] comments = postData[2].Split('>');
                                    foreach (string com in comments)
                                    {
                                        string[] user_comment = com.Split(':');
                                        User b = new User(user_comment[0]);
                                        if (!P.Getcomments().ContainsKey(b))
                                            P.Getcomments()[b] = new List<string>();
                                        P.Getcomments()[b].Add(com);
                                    }
                                }
                                activeUser.GetPosts().Add(P);
                            }

                            Label dynamicLabel = new Label();
                            dynamicLabel.Text = post_time[1]; // timestamp
                            dynamicLabel.ForeColor = Color.Gray;
                            dynamicLabel.AutoSize = true;
                            dynamicLabel.Location = new Point(3, yOffset1);
                            uc.Controls.Add(dynamicLabel);

                            Label dynamicLabel1 = new Label();
                            dynamicLabel1.Text = a.GetUsername() + "'s post:\n" + post_time[0];
                            dynamicLabel1.AutoSize = true;
                            dynamicLabel1.Location = new Point(3, yOffset11);
                            uc.Controls.Add(dynamicLabel1);

                            Button LikeButton = new Button();
                            LikeButton.Size = new Size(70, 25);
                            LikeButton.Location = new Point(3, yOffset2);
                            LikeButton.Text = "Like";
                            LikeButton.BackColor = Color.White;
                            LikeButton.Tag = P;
                            LikeButton.Click += LikeButton_click;
                            uc.Controls.Add(LikeButton);

                            Button CommentsButton = new Button();
                            CommentsButton.Size = new Size(70, 25);
                            CommentsButton.Location = new Point(80, yOffset2);
                            CommentsButton.Text = "Comments";
                            CommentsButton.BackColor = Color.White;
                            CommentsButton.Tag = postData[1];
                            CommentsButton.Click += CommentsButton_click;
                            uc.Controls.Add(CommentsButton);

                            yOffset1 += 75;
                            yOffset2 += 75;
                            yOffset11 += 75;
                        }
                    }
                }
            }
            if(!testpost)
            {
                Label dynamicLabel = new Label();

                dynamicLabel.Text = "there's no posts yet";
                dynamicLabel.Size = new Size(300, 30);
                dynamicLabel.Location = new Point(365, 192);

                uc.Controls.Add(dynamicLabel);
            }
            // close
            addUserControl(uc);
        } 

        private void RequestButton_Click(object sender, EventArgs e)
        {
            FriendsPanel uc = new FriendsPanel();
            // imp
            int yOffset = 50;

            if ( activeUser.GetRequest().Count != 0 )
            {
                foreach (User req in activeUser.GetRequest())
                {
                    Label dynamicLabel = new Label();

                    dynamicLabel.Text = req.GetUsername();
                    dynamicLabel.Size = new Size(200, 30);
                    dynamicLabel.Location = new Point(50, yOffset);
                    uc.Controls.Add(dynamicLabel);

                    Button dynamicButton = new Button();
                    dynamicButton.Text = "accept friend";
                    dynamicButton.Size = new Size(200, 30);
                    dynamicButton.Location = new Point(250, yOffset);
                    dynamicButton.Tag = req; // Store req in the Tag property
                    dynamicButton.Click += AcceptFriendButton_Click; // Assign click event
                    uc.Controls.Add(dynamicButton);

                    yOffset += 40;
                }
            }
            else
            {
                Label dynamicLabel = new Label();

                dynamicLabel.Text = "there is no friend request";
                dynamicLabel.Size = new Size(300, 30);
                dynamicLabel.Location = new Point(365, 192);
                uc.Controls.Add(dynamicLabel);
            }
            // close
            addUserControl(uc);
        } 

        private void SearchButton_Click(object sender, EventArgs e)
        {
            SearchPanel uc = new SearchPanel();
            // imp
            string path = "UsersInfo.txt";
            string[] lines = File.ReadAllLines(path);
            int yOffset = 50;
            bool userFound = false;
            
            for (int i = 0; i < lines.Length; i++)
            {
                string[] userData = lines[i].Split('|');
                if (userData.Length > 0 && userData[0] != activeUser.GetUsername())
                {
                    bool exists = false;
                    User userToDisplay = new User(userData[0]);

                    if (userData[3] != "")
                    {
                        string[] request = userData[3].Split('>');
                        foreach (string req in request)
                        {
                            User a = new User(req);
                            userToDisplay.GetRequest().Add(a);
                        }
                        exists = userToDisplay.GetRequest().Any(user => user.GetUsername() == activeUser.GetUsername());
                    }
                    bool exists1 = activeUser.GetRequest().Any(user => user.GetUsername() == userToDisplay.GetUsername());//bool exists1 = activeUser.GetRequest().Contains(userToDisplay);
                    bool exists2 = activeUser.GetFriends().Any(user => user.GetUsername() == userToDisplay.GetUsername());//bool exists2 = activeUser.GetFriends().Contains(userToDisplay);
                    if ((!exists1 && !exists2) && !exists)
                    {
                        userFound = true;
                        Label dynamicLabel = new Label();

                        dynamicLabel.Text = userToDisplay.GetUsername();
                        dynamicLabel.Size = new Size(200, 30);
                        dynamicLabel.Location = new Point(50, yOffset);
                        uc.Controls.Add(dynamicLabel);

                        Button dynamicButton = new Button();
                        dynamicButton.Text = "Add friend";
                        dynamicButton.Size = new Size(200, 30);
                        dynamicButton.Location = new Point(250, yOffset);
                        dynamicButton.Tag = userData[0]; // Store username in the Tag property
                        dynamicButton.Click += AddFriendButton_Click; // Assign click event
                        uc.Controls.Add(dynamicButton);

                        yOffset += 40;
                    }
                }
            }
            if(!userFound)
            {
                Label dynamicLabel = new Label();

                dynamicLabel.Text = "All Users in this app are discovered";
                dynamicLabel.AutoSize = true;
                dynamicLabel.Location = new Point(365, 192);
                uc.Controls.Add(dynamicLabel);
            }
            //close
            addUserControl(uc);
        } 

        private void ProfileButton_Click(object sender, EventArgs e)
        {
            ProfilePanel uc = new ProfilePanel();
            //imp
            int ypp = 162;
            int ylc = 193;

            Label dynamicLabel = new Label();
            dynamicLabel.Text = activeUser.GetUsername();
            dynamicLabel.Font = new Font("Century Gothic", 20, FontStyle.Bold);
            dynamicLabel.AutoSize = true;
            dynamicLabel.Location = new Point(140,53);
            uc.Controls.Add(dynamicLabel);

            Button dynamicButton = new Button();
            dynamicButton.Text = "Friends";
            dynamicButton.Size = new Size(71, 25);
            dynamicButton.Location = new Point(42,116);
            dynamicButton.Click += Friends_Click; // Assign click event
            uc.Controls.Add(dynamicButton);

            Label dynamicLabel2 = new Label();
            dynamicLabel2.Text = "Posts";
            dynamicLabel2.Font = new Font("Century Gothic", 12, FontStyle.Bold);
            dynamicLabel2.AutoSize = true;
            dynamicLabel2.Location = new Point(140, 139);
            uc.Controls.Add(dynamicLabel2);

            if(activeUser.GetPosts().Count != 0)
            {
                foreach(Post P in activeUser.GetPosts())
                {
                    Label dynamicLabel7 = new Label();
                    dynamicLabel7.Text = P.GetTimestamp().ToString(); // timestamp
                    dynamicLabel7.ForeColor = Color.Gray;
                    dynamicLabel7.AutoSize = true;
                    dynamicLabel7.Location = new Point(140, ypp);
                    uc.Controls.Add(dynamicLabel7);

                    Label dynamicLabel5 = new Label();
                    dynamicLabel5.Text = P.GetContent();
                    dynamicLabel5.AutoSize = true;
                    dynamicLabel5.Location = new Point(140, 15 + ypp);
                    uc.Controls.Add(dynamicLabel5);

                    Button dynamicButton2 = new Button();
                    dynamicButton2.Text = "Likes";
                    dynamicButton2.Size = new Size(71, 25);
                    dynamicButton2.Location = new Point(140, ylc);
                    dynamicButton2.Tag = P;
                    dynamicButton2.Click += Likes_Click; // Assign click event
                    uc.Controls.Add(dynamicButton2);

                    Button dynamicButton3 = new Button();
                    dynamicButton3.Text = "Comments";
                    dynamicButton3.Size = new Size(71, 25);
                    dynamicButton3.Location = new Point(230, ylc);
                    string content_in_file = P.GetContent() + "<" + P.GetTimestamp().ToString();
                    dynamicButton3.Tag = content_in_file;
                    dynamicButton3.Click += CommentsButton_click; // Assign click event
                    uc.Controls.Add(dynamicButton3);

                    ypp += 62;
                    ylc += 62;
                }
            }
            else
            {
                Label dynamicLabel4 = new Label();
                dynamicLabel4.Text = "you don't have Posts yet :(";
                dynamicLabel4.AutoSize = true;
                dynamicLabel4.Location = new Point(145, 235); //kenet 100 l x
                uc.Controls.Add(dynamicLabel4);
            }
            //close
            addUserControl(uc);
        }

        private void Likes_Click(object sender, EventArgs e)
        {
            if (sender is Button button)
            {
                ShowLikesPanel uc = new ShowLikesPanel();
                // imp
                Post P = ((Button)sender).Tag as Post;
                int yOffset = 80;

                Label dynamicLabel7 = new Label();
                dynamicLabel7.Text = P.GetTimestamp().ToString(); // timestamp
                dynamicLabel7.ForeColor = Color.Gray;
                dynamicLabel7.AutoSize = true;
                dynamicLabel7.Location = new Point(33, 23);
                uc.Controls.Add(dynamicLabel7);

                Label dynamicLabel5 = new Label();
                dynamicLabel5.Text = P.GetContent();
                dynamicLabel5.AutoSize = true;
                dynamicLabel5.Location = new Point(33, 35+3);
                uc.Controls.Add(dynamicLabel5);

                Label dynamicLabel1 = new Label();
                dynamicLabel1.Text = "Liked By:";
                dynamicLabel1.Font = new Font("Century Gothic", 12, FontStyle.Bold);
                dynamicLabel1.AutoSize = true;
                dynamicLabel1.Location = new Point(33, 60);
                uc.Controls.Add(dynamicLabel1);
                dynamicLabel1.Hide();

                if(P.GetLikes().Count != 0)
                {
                    dynamicLabel1.Show();
                    foreach (User usr in P.GetLikes())
                    {
                        Label dynamicLabel2 = new Label();
                        dynamicLabel2.Text = usr.GetUsername();
                        dynamicLabel2.Font = new Font("Century Gothic", 10, FontStyle.Regular);
                        dynamicLabel2.AutoSize = true;
                        dynamicLabel2.Location = new Point(33, yOffset);
                        uc.Controls.Add(dynamicLabel2);
                        yOffset += 20;
                    }
                }
                else                
                {
                    Label dynamicLabel2 = new Label();
                    dynamicLabel2.Text = "unfortunately No Likes";
                    dynamicLabel2.Font = new Font("Century Gothic", 12, FontStyle.Regular);
                    dynamicLabel2.AutoSize = true;
                    dynamicLabel2.Location = new Point(33, 200);
                    uc.Controls.Add(dynamicLabel2);
                }
                // close
                uc.Dock = DockStyle.Fill;
                ContainerPanel.Controls.Add(uc);
                uc.BringToFront();
            }
                
        }

        private void Friends_Click(object sender, EventArgs e)
        {
            MyFriendPanel friendPanel = new MyFriendPanel();
            //imp
            if (activeUser.GetFriends().Count != 0)
            {
                Label dynamicLabel = new Label();
                dynamicLabel.Text = "Friends: ";
                dynamicLabel.Font = new Font("Century Gothic", 12, FontStyle.Bold);
                dynamicLabel.AutoSize = true;
                dynamicLabel.Location = new Point(33,23);
                friendPanel.Controls.Add(dynamicLabel);
                int yf = 45;
                foreach (User usr in activeUser.GetFriends())
                {
                    Label dynamicLabel1 = new Label();
                    dynamicLabel1.Text = usr.GetUsername();
                    dynamicLabel1.Font = new Font("Century Gothic", 10, FontStyle.Regular);
                    dynamicLabel1.AutoSize = true;
                    dynamicLabel1.Location = new Point(33, yf);
                    friendPanel.Controls.Add(dynamicLabel1);
                    yf += 20;
                }
            }
            else
            {
                Label dynamicLabel1 = new Label();
                dynamicLabel1.Text = "there's no friends yet :(";
                dynamicLabel1.Font = new Font("Century Gothic", 10, FontStyle.Regular);
                dynamicLabel1.AutoSize = true;
                dynamicLabel1.Location = new Point(365, 192);
                friendPanel.Controls.Add(dynamicLabel1);
            }
            //close
            friendPanel.Dock = DockStyle.Fill;
            ContainerPanel.Controls.Add(friendPanel);
            friendPanel.BringToFront();
        }

        private void AddFriendButton_Click(object sender, EventArgs e)
        {
            if (sender is Button button) // Check if the sender is a Button
            {
                string usernameWhereToAdd = button.Tag.ToString();

                string path = "UsersInfo.txt";
                string[] lines = File.ReadAllLines(path);

                for (int i = 0; i < lines.Length; i++)
                {
                    string[] userData = lines[i].Split('|');
                    if (userData.Length >= 4 && userData[0] == usernameWhereToAdd)
                    {
                        if (userData[3] == "") // If the requests field is empty
                            lines[i] = userData[0] + "|" + userData[1] + "|" + userData[2] + "|" + activeUser.GetUsername();
                        else
                            lines[i] = userData[0] + "|" + userData[1] + "|" + userData[2] + "|" + userData[3] + ">" + activeUser.GetUsername();
                        
                        File.WriteAllLines(path, lines);
                        break;
                    }
                }
                button.Enabled = false;
            }
        }

        private void AcceptFriendButton_Click(object sender, EventArgs e)
        {
            if (sender is Button button) // Check if the sender is a Button
            {
                User userToAdd = ((Button)sender).Tag as User;

                activeUser.GetFriends().Add(userToAdd); // Add to the list of friends
                activeUser.GetRequest().Remove(userToAdd); // Remove from the list of requests
                //userToAdd.GetFriends().Add(activeUser); // doesn't matter i have to do it in the file

                string path = "UsersInfo.txt"; // do the same in the file && add the activeuser to the friends of usernameToAdd
                string[] lines = File.ReadAllLines(path);
                for (int i = 0; i < lines.Length; i++)
                {
                    string[] userData = lines[i].Split('|');
                    if (userData.Length >= 4 && userData[0] == activeUser.GetUsername())
                    {
                        string[] requests = userData[3].Split('>');
                        requests = Array.FindAll(requests, username => username != userToAdd.GetUsername());// Remove the user from the array of requests
                        userData[3] = string.Join(">", requests);// Reconstruct the string without the removed user

                        if (userData[2] == "") // If the Friends field is empty
                            lines[i] = userData[0] + "|" + userData[1] + "|" + userToAdd.GetUsername() + "|" + userData[3];
                        else
                            lines[i] = userData[0] + "|" + userData[1] + "|" + userData[2] + ">" + userToAdd.GetUsername() + "|" + userData[3];
                        
                        File.WriteAllLines(path, lines);
                    }
                    else
                    {
                        if (userData.Length >= 4 && userData[0] == userToAdd.GetUsername())
                        {
                            if (userData[2] == "") // If the Friends field is empty
                                lines[i] = userData[0] + "|" + userData[1] + "|" + activeUser.GetUsername() + "|" + userData[3];
                            else
                                lines[i] = userData[0] + "|" + userData[1] + "|" + userData[2] + ">" + activeUser.GetUsername() + "|" + userData[3];
                            
                            File.WriteAllLines(path, lines);
                        }
                    }
                }
                button.Enabled = false;
            }
        }
         
        private void CreatePostButton_click(object sender, EventArgs e)
        {
            if (sender is Button button)
            {
                string postContent = button.Tag.ToString();
                DateTime timestamp= DateTime.Now;
                activeUser.GetPosts().Add( new Post(postContent, timestamp) );
                try
                {
                    string path = "UsersPosts.txt";
                    if (!File.Exists(path))
                    {
                        StreamWriter Pen = new StreamWriter(path);
                        Pen.Write(activeUser.GetUsername() + "|" + postContent + "<" + timestamp + "|" + "|");
                        Pen.Write("\n");
                        Pen.Close();
                    }
                    else
                    {
                        StreamWriter Pen = File.AppendText(path);
                        Pen.Write(activeUser.GetUsername() + "|" + postContent + "<" + timestamp + "|" + "|");
                        Pen.Write("\n");
                        Pen.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                HomeButton.PerformClick();
            }
        } 

        private void LikeButton_click(object sender, EventArgs e)
        {
            if (sender is Button button)
            {
                Post P = ((Button)sender).Tag as Post;
                string contentWhereToAdd = P.GetContent() + "<" + P.GetTimestamp().ToString();
                string path = "UsersPosts.txt";
                string[] lines = File.ReadAllLines(path);
                for (int i = 0; i < lines.Length; i++)
                {
                    string[] userData = lines[i].Split('|');
                    if (userData.Length > 0 && userData[1] == contentWhereToAdd)
                    {
                        if (userData[2] == "")
                        {
                            P.GetLikes().Add(activeUser);
                            lines[i] = userData[0] + "|" + userData[1] + "|" + activeUser.GetUsername() + "|" + userData[2];
                            File.WriteAllLines(path, lines);
                        }
                        else
                        {
                            string[] likes = userData[2].Split('>');
                            bool found = false;

                            foreach (string l in likes)
                            {
                                if (l == activeUser.GetUsername())
                                {
                                    found = true;
                                    break;
                                }
                            }

                            if (!found)
                            {
                                P.GetLikes().Add(activeUser);
                                lines[i] = userData[0] + "|" + userData[1] + "|" + userData[2] + ">" + activeUser.GetUsername() + "|" + userData[3];
                                File.WriteAllLines(path, lines);
                            }
                        }
                        break;
                    }
                }
                button.Enabled = false;
            }

        }

        private void CommentsButton_click(object sender, EventArgs e)
        {
            if (sender is Button button)
            {
                CommentsPanel uc = new CommentsPanel();
                // imp
                string contentWhereToshow = button.Tag.ToString();
                string[] post_time = contentWhereToshow.Split('<');

                Label dynamicLabel3 = new Label();
                dynamicLabel3.Text = post_time[1]; // timestamp
                dynamicLabel3.ForeColor = Color.Gray;
                dynamicLabel3.AutoSize = true;
                dynamicLabel3.Location = new Point(3, 3);
                uc.Controls.Add(dynamicLabel3);

                Label dynamicLabel2 = new Label();
                dynamicLabel2.Text = post_time[0];
                dynamicLabel2.AutoSize = true;
                dynamicLabel2.Location = new Point(3,23);
                uc.Controls.Add(dynamicLabel2);

                // Add comment
                TextBox AddCommentsTextBox = new TextBox();
                AddCommentsTextBox.Size = new Size(485, 25);
                AddCommentsTextBox.Location = new Point(90, 50);
                uc.Controls.Add(AddCommentsTextBox);

                Button AddCommentsButton = new Button();
                AddCommentsButton.Size = new Size(82, 25);
                AddCommentsButton.Location = new Point(3, 46);
                AddCommentsButton.Text = "Add Comment";
                AddCommentsButton.BackColor = Color.White;

                AddCommentsButton.Click += (s, ev) =>
                {
                    string commentText = AddCommentsTextBox.Text;
                    string[] myArray = { contentWhereToshow, commentText};
                    AddCommentsButton.Tag = myArray;
                    AddCommentsTextBox.Text = "";
                };
                AddCommentsButton.MouseDown += (s, ev) =>
                {
                    AddCommentsButton.BackColor = Color.Red;
                };

                AddCommentsButton.MouseUp += (s, ev) =>
                {
                    AddCommentsButton.BackColor = Color.White;
                };

                AddCommentsButton.Click += AddCommentsButton_click;
                uc.Controls.Add(AddCommentsButton);
                // show comments
                string path = "UsersPosts.txt";
                string[] lines = File.ReadAllLines(path);
                int yOffset = 78;
                for (int i = 0; i < lines.Length; i++)
                {
                    string[] postData = lines[i].Split('|');
                    if (postData.Length > 0 && postData[1] == contentWhereToshow)
                    {
                        if (postData[3] != "")
                        {
                            string[] comments_ = postData[3].Split('>');
                            foreach(string com in comments_)
                            {
                                Label dynamicLabel1 = new Label();
                                dynamicLabel1.Text = com;
                                dynamicLabel1.Size = new Size(600, 20);
                                dynamicLabel1.Location = new Point(3, yOffset);
                                uc.Controls.Add(dynamicLabel1);
                                yOffset += 25;
                            }
                        }
                        else
                        {
                            Label dynamicLabel1 = new Label();
                            dynamicLabel1.Text = "No comments";
                            dynamicLabel1.Size = new Size(600, 20);
                            dynamicLabel1.Location = new Point(400, 200);
                            uc.Controls.Add(dynamicLabel1);
                        }
                    }
                }
                // close
                uc.Dock = DockStyle.Fill;
                ContainerPanel.Controls.Add(uc);
                uc.BringToFront();
            }
        }

        private void AddCommentsButton_click(object sender, EventArgs e)
        {
            if(sender is Button button)
            {
                string[] myArray = button.Tag as string[];
                string path = "UsersPosts.txt";
                string[] lines = File.ReadAllLines(path);
                for (int i = 0; i < lines.Length; i++)
                {
                    string[] userData = lines[i].Split('|');
                    if (userData.Length > 0 && userData[1] == myArray[0])
                    {
                        if (userData[3] == "") // If the comments field is empty
                            lines[i] = userData[0] + "|" + userData[1] + "|" + userData[2] + "|" + activeUser.GetUsername() + ": " + myArray[1];
                        else
                            lines[i] = userData[0] + "|" + userData[1] + "|" + userData[2] + "|" + userData[3] + ">" + activeUser.GetUsername() + ": " + myArray[1];
                        File.WriteAllLines(path, lines);
                        break;
                    }
                }
            }
        }

        private void MyApp_Load(object sender, EventArgs e)
        {
            HomeButton.PerformClick();
            GetRequestFrom(activeUser);
        }

        private void GetRequestFrom(User a)
        {
            string path = "UsersInfo.txt";
            string[] lines = File.ReadAllLines(path);
            a.GetRequest().Clear();

            for (int i = 0; i < lines.Length; i++)
            {
                string[] userData = lines[i].Split('|');
                if (userData.Length > 0 && userData[0] == a.GetUsername())
                {
                    if (userData[3] != "")
                    {
                        string[] requests = userData[3].Split('>');
                        foreach (string req in requests)
                        {
                            User b = new User(req);
                            a.GetRequest().Add(b);
                        }
                    }
                }
            }
        }
        
    }
}
