# TweetsAPI

Projeto desenvolvido em .netCore 3.0 para ler os tweets gerados pelo projeto [twitterConsumer](https://github.com/marceloapps/twitter-consumer) e expor os dados em 3 endpoints para serem consumidos:
* maxfollowers
* tweetsgrouped
* tweetsbyhashtag

## Inicialização

Clonar o repositório TweetsAPI para a máquina local, e abrir com o Visual Studio 2019 (ou VS Code).

### Pré requisitos

* [VS 2019](https://visualstudio.microsoft.com/pt-br/thank-you-downloading-visual-studio/?sku=Community&rel=16)
* [SDK do dot.net core 3.0](https://dotnet.microsoft.com/download/dotnet-core/thank-you/sdk-3.0.101-windows-x64-installer)
* [Windows Hosting](https://www.microsoft.com/net/permalink/dotnetcore-current-windows-runtime-bundle-installer)
* [ElasticSearch](https://artifacts.elastic.co/downloads/elasticsearch/elasticsearch-7.4.2-windows-x86_64.zip)
* [Kibana](https://artifacts.elastic.co/downloads/kibana/kibana-7.4.2-windows-x86_64.zip)
* [Prometheus](https://github.com/prometheus/prometheus/releases/download/v2.14.0/prometheus-2.14.0.windows-amd64.tar.gz)

#### Para iniciar o ElasticSearch:
Navegar até a pasta com os arquivos extraídos e executar: ".\elasticsearch.bat"

#### Para iniciar o Kibana
Navegar até a pasta com os arquivos extraídos e executar: ".\bin\kibana.bat"

#### Para iniciar o Prometheus
Configurar o arquivo prometheus.yml na pasta dos arquivos extraídos para escutar a url do TwitterAPI e não do próprio Prometheus
Navegar até a pasta com os arquivos extraídos e executar: ".\prometheus --config.file=prometheus.yml"

### Instalação

Com o projeto aberto, usar a opção "publish" do Visual Studio.
Se na máquina houver o .net core runtime, pode selecionar a opção "framework dependent". Caso contrário, selecionar "autosuficient".
Caso deseje, copie os arquivos gerados na pasta \bin\Release\netcoreapp2.2\publish a um local definitivo.

* Abra o IIS (tecla windows+R e digitar inetmgr)
* Clique com o botão direito em "Pool de aplicativos" e selecione "Adicionar Pool de Aplicativos". Na tela que se abrir, de o nome de "DotNetCore", selecione "Sem código gerenciado" na opção "Versão .Net CLR" e clique em OK
* Adicione novo site com o nome de TweetsAPI, selecione o pool de aplicativos que foi criado e coloque na porta 3001. Feito isso, clique OK

## Execução

No navegador, testar as seguintes urls:
* http://localhost:3001/api/tweet/ - retorna a lista de todos os tweets coletados
* http://localhost:3001/api/tweet/maxfollowers - retorna a lista dos usuários com maior número de seguidores
* http://localhost:3001/api/tweet/tweetsgrouped - retorna a soma de tweets agrupados por hora
* http://localhost:3001/api/tweet/tweetsbyhashtag - retorna a soma de tweets por hashtag/idioma

## Built With

* [.NetCore 3.0](https://docs.microsoft.com/pt-br/dotnet/core/about) - Framework utilizado
* [MongoDB.Driver](https://docs.mongodb.com/ecosystem/drivers/csharp/) - Utilizado para criar conexão com mongodb
* [Prometheus-net] - Gerar métricas de execução
* [ElasticSearch] - Logging

## Versioning

v1.0.0

## Autor

* **Marcelo Arakaki** - [marceloapps](https://github.com/marceloapps)
