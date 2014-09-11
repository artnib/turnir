namespace turnir
{
  /// <summary>
  /// Названия настроек формы
  /// </summary>
  static class Setting
  {
    #region Главная форма

    /// <summary>
    /// Расстояние от левого края экрана
    /// </summary>
    internal const string Left = "Left";
    
    /// <summary>
    /// Расстояние от верхнего края экрана
    /// </summary>
    internal const string Top = "Top";
    
    /// <summary>
    /// Высота
    /// </summary>
    internal const string Height = "Height";
    
    /// <summary>
    /// Ширина
    /// </summary>
    internal const string Width = "Width";

    internal const string Maximized = "Maximized";

    /// <summary>
    /// Ширина столбцов личной таблицы
    /// </summary>
    internal const string Columns = "Columns";

    /// <summary>
    /// Ширина столбцов командной таблицы
    /// </summary>
    internal const string ColumnsTeam = "ColumnsTeam";

    /// <summary>
    /// Файл последнего турнира
    /// </summary>
    internal const string LastFile = "LastFile";

    /// <summary>
    /// Индекс выбранной вкладки
    /// </summary>
    internal const string LastTab = "TabIndex";

    /// <summary>
    /// Индекс выбранной таблицы
    /// </summary>
    internal const string TableIndex = "TableIndex";

    #endregion

    #region Форма партий

    /// <summary>
    /// Расстояние от левого края экрана до формы партий
    /// </summary>
    internal const string GameLeft = "GameLeft";

    /// <summary>
    /// Расстояние от верхнего края экрана до формы партий
    /// </summary>
    internal const string GameTop = "GameTop";

    /// <summary>
    /// Ширина формы партий
    /// </summary>
    internal const string GameWidth = "GameWidth";

    /// <summary>
    /// Высота формы партий
    /// </summary>
    internal const string GameHeight = "GameHeight";

    #endregion

    #region Форма команды

    /// <summary>
    /// Расстояние от левого края экрана до формы команды
    /// </summary>
    internal const string TeamLeft = "TeamLeft";
    
    /// <summary>
    /// Расстояние от верхнего края экрана до формы команды
    /// </summary>
    internal const string TeamTop = "TeamTop";
    
    /// <summary>
    /// Ширина формы команды
    /// </summary>
    internal const string TeamWidth = "TeamWidth";
    
    /// <summary>
    /// Высота формы команды
    /// </summary>
    internal const string TeamHeight = "TeamHeight";

    #endregion
  }
}