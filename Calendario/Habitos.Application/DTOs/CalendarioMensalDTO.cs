using System;

namespace Habitos.Application.DTOs;

public record CalendarioMensalDTO(int Ano, int Mes, List<DiaCalendarioDTO> Dias);   