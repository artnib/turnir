namespace turnir
{
  /// <summary>
  /// Результат партии
  /// </summary>
  enum GameResult
  { 
    /// <summary>
    /// Партия не сыграна
    /// </summary>
    None,
    /// <summary>
    /// Выигрыш белых
    /// </summary>
    White,
    /// <summary>
    /// Выигрыш чёрных
    /// </summary>
    Black,
    /// <summary>
    /// Ничья
    /// </summary>
    Draw,
    /// <summary>
    /// Неявка/опоздание белых
    /// </summary>
    WhiteForfeit,
    /// <summary>
    /// Неявка/опоздание чёрных
    /// </summary>
    BlackForfeit
  }
}