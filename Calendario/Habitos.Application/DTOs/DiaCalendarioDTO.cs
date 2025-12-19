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

// Um record (registro) em C# é uma estrutura similar a uma struct em C.
// Usar um record ao invés de uma classe para um DTO ajuda 
// na simplicidade de transferência de dados.