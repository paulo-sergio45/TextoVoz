
# Aplicativo Texto Voz

O aplicativo foi desenvolvido para ler um arquivo de texto através da interface text-to-speech do .NET MAUI e reproduzir o áudio.


## Features

- Text-to-speech (TTS)
- Ajustes de voz, volume e idioma.
- O reprodutor de áudio possui funções para iniciar, avançar e voltar.

## Teste

- Windows
- Android

## Deployment

Para instalar o aplicativo, você precisa assinar usando uma chave gerada pelo keytool do JDK, disponível no prompt de comando na pasta da aplicação.

1º Criar uma chave.
```bash
keytool -genkeypair -v -keystore {filename}.keystore -alias {keyname} -keyalg RSA -keysize 2048 -validity 10000
```
2º usando a chave no publish.
```bash
dotnet publish TextoVoz.csproj -f net8.0-android -c Release -p:AndroidKeyStore=true -p:AndroidSigningKeyStore={filename}.keystore -p:AndroidSigningKeyAlias={keyname} -p:AndroidSigningKeyPass={password} -p:AndroidSigningStorePass={password}
```

## Authors

- [@paulo-sergio45 ](https://github.com/paulo-sergio45)
