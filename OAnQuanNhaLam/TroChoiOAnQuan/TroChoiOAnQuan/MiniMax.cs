using System;
using System.Collections.Generic;
using System.Linq;
using TroChoiOAnQuan;
public class MiniMax
{
    public static string user;

    public static void RunFunction()
    {
        OAnQuan board = new OAnQuan();
        bool aiTurn = false;

        //Console.WriteLine("Choose a player (Player1 or Player2): ");
        user = "Player1";
        string ai = user == "Player1" ? "Player2" : "Player1";

        while (true)
        {
            bool gameOver = Terminal(board);
            string currentPlayer = Player(board);

            if (gameOver)
            {
                string winner = Winner(board);
                if (winner == null)
                {
                    //Console.WriteLine("Game Over: Tie.");

                }
                else
                {
                    Console.WriteLine("Game Over: {0} wins.", winner);
                }
                break;
            }
            else
            {
                if (user != currentPlayer && !gameOver)
                {
                    if (aiTurn)
                    {
                        NuocDiChuyen move = Minimax(board, 5);
                        Console.WriteLine("AI play vi tri " + move.Vitri + " va huong di chuyen la " + move.DiTraiPhai);
                        board = ConnectOAnQuan.ThucHienNuocDi(board, move);
                        aiTurn = false;
                        board.xuat();
                    }
                }
                else if (user == currentPlayer && !gameOver)
                {
                    aiTurn = true;
                    while (true)
                    {
                        Console.WriteLine("Enter your move (Vi tri di quan): ");
                        int vitri = int.Parse(Console.ReadLine());
                        Console.WriteLine("Enter your huong di : RIGHT | LEFT ");
                        string huongdi = Console.ReadLine();
                        NuocDiChuyen nuocdi = new NuocDiChuyen(vitri, huongdi);
                        if (IsValidMove(nuocdi, board))
                        {
                            ConnectOAnQuan.ThucHienNuocDi(board, nuocdi);
                            board.xuat();
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Invalid move. Please try again.");
                        }
                    }
                }
            }
        }
        Console.ReadLine();
    }

    public static string Player(OAnQuan board)
    {
        if (board.CheckNguoiChoi == -1)
        {
            return "Player1";
        }
        return "Player2";
    }

    public static string Winner(OAnQuan board)
    {
        int diemPlayer1 = board.Player1.SoDanDaAn + board.Player1.SoQuanDaAn * 5;
        int diemPlayer2 = board.Player2.SoDanDaAn + board.Player2.SoQuanDaAn * 5;
        if (diemPlayer1 > diemPlayer2)
        {
            return "Player1";
        }
        if (diemPlayer2 > diemPlayer1)
        {
            return "Player2";
        }
        return null;
    }

    public static bool Terminal(OAnQuan board)
    {
        return ConnectOAnQuan.IsGameOver(board) || ConnectOAnQuan.mustRai5ODan(board);
    }

    public static int Utility(OAnQuan board)
    {
        string winner = Winner(board);
        if (winner == "Player1")
        {
            return 1;
        }
        else if (winner == "Player2")
        {
            return -1;
        }
        return 0;
    }

    public static int MaxValue(OAnQuan state, int depth)
    {
        if (depth == 0 || Terminal(state))
        {
            return Utility(state);
        }
        int v = int.MinValue;
        foreach (NuocDiChuyen action in state.Actions())
        {
            OAnQuan temp = state.Clone();
            v = Math.Max(v, MinValue(ConnectOAnQuan.ThucHienNuocDi(temp, action), depth - 1));
        }
        return v;
    }

    public static int MinValue(OAnQuan state, int depth)
    {
        if (depth == 0 || Terminal(state))
        {
            return Utility(state);
        }
        int v = int.MaxValue;
        foreach (NuocDiChuyen action in state.Actions())
        {
            OAnQuan temp = state.Clone();
            v = Math.Min(v, MaxValue(ConnectOAnQuan.ThucHienNuocDi(temp, action), depth - 1));
        }
        return v;
    }

    public static NuocDiChuyen Minimax(OAnQuan board, int depth)
    {
        string currentPlayer = Player(board);
        int min = int.MinValue;
        int max = int.MaxValue;
        NuocDiChuyen move = new NuocDiChuyen();

        if (currentPlayer == "Player1")
        {
            min = int.MinValue;
            foreach (NuocDiChuyen action in board.Actions())
            {
                OAnQuan temp = board.Clone();
                int check = MinValue(ConnectOAnQuan.ThucHienNuocDi(temp, action), depth);
                if (check > min)
                {
                    min = check;
                    move = action;
                }
            }
        }
        else
        {
            max = int.MaxValue;
            foreach (NuocDiChuyen action in board.Actions())
            {
                OAnQuan temp = board.Clone();
                int check = MaxValue(ConnectOAnQuan.ThucHienNuocDi(temp, action), depth);
                if (check < max)
                {
                    max = check;
                    move = action;
                }
            }
        }

        return move;
    }

    public static bool IsValidMove(NuocDiChuyen nuocdi, OAnQuan board)
    {
        if (board.CheckNguoiChoi == -1)
        {
            if (nuocdi.Vitri >= 1 && nuocdi.Vitri <= 5)
                return true;
            else
                return false;
        }
        else
        {
            if (nuocdi.Vitri >= 7 && nuocdi.Vitri <= 11)
                return true;
            else
                return false;
        }
    }
}