if (start_node[0] > 0)
{
    if (grid[start_node[0] - 1, start_node[1]] == 2)
    {
        start_node[0]--;
        list_cheked_node = check_node(row, col, start_node);
    }
}
else if (start_node[1] > 0)
{
    if ((grid[start_node[0], start_node[1]] == 2))
    {
        start_node[1]--;
        list_cheked_node = check_node(row, col, start_node);
    }
}
else if (start_node[0] < row - 1)
{
    if ((grid[start_node[0], start_node[1]] == 2))
    {
        start_node[0]++;
        list_cheked_node = check_node(row, col, start_node);
    }
}
else if (start_node[1] < col - 1)
{
    if (Program.visted_node[node.Row, node.Col])
    {
        start_node[1]++;
        list_cheked_node = check_node(row, col, start_node);
    }
}