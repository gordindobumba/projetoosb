using System;

namespace Habitos.Application.DTOs;

public record DiaCalendarioDTO(
    int Dia, 
    List<string> Habitos,
    int habitosTotais,
    int habitosCompletados,
    double porcentagemCompletados,
    string Cor // nome ou valor hexadecimal
);

// Um record (registro) é similar a uma classe, porém é imutável e pode ser comparado 
// com valores ao invés de variáveis de referência.
// Ajuda na simplicidade de transferência de dados.