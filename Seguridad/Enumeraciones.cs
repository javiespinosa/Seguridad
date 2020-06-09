namespace Seguridad
{
	using System.ComponentModel;
	public enum TipoArmadoMenu
	{

		[Description("Aplicaciones")]
		Aplicaciones = 1,
		[Description("Modulo")]
		Modulo = 2
	}
	public enum PistaColor
	{

		[Description("Verde")]
		Verde = 1,
		[Description("Amarilla")]
		Amarilla = 2,
		[Description("Roja")]
		Roja = 3,
		[Description("Negra")]
		Negra = 4

	}
	public enum PistaTipo
	{
		[Description("Info Sistema")]
		Sistema = 0,
		[Description("Acceso")]
		Acceso = 1,
		[Description("Consulta")]
		Consulta = 2,
		[Description("Agregar")]
		Agregar = 3,
		[Description("Editar")]
		Editar = 4,
		[Description("Eliminar")]
		Eliminar = 5,
		[Description("Transición")]
		Transicion = 6,
		[Description("Eje")]
		Eje = 7
	}

	public enum EnumUnidadesAdministrativas
	{

		[Description("Oficina Tuxtla Gutierrez")]
		Tuxtla = 1,
		[Description("Oficina Tapachula")]
		Tapachula = 2
	}
	//public enum EnumAplicaciones
	//{

	//    [Description("Contabilidad Presupuestal")]
	//    ContabilidadPresupuestal = 1,
	//    [Description("Contabilidad")]
	//    Contabilidad = 2,
	//    [Description("Recursos Materiales")]
	//    RecursosMateriales = 3,
	//    [Description("Tesoreria")]
	//    Tesoreria = 4,
	//    [Description("Licitaciones")]
	//    Licitaciones = 5,
	//    [Description("Seguridad")]
	//    Seguridad = 6,
	//}
}