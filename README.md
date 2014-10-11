CommonDatabaseAccess
====================

通用的数据库操作类库


使用说明
该类库可以用来访问oracle、 sql server 、mysql、sqlite数据库
因为访问不同数据库使用的类不同，但是却是通过同一个工厂类调用的，
那么怎么样去体现不同数据库调用不同的操作类，那么我们就需要在配置文件中
进行说明：如：
oracle数据库=> 对应的factoryName 为 OracleDBA 同时也要将那个连接字符串改成oracle数据库使用的连接字符串
sql server数据库=>对应的factoryName 为 SqlServerDBA 同时也要将那个连接字符串改成sql server数据库使用的连接字符串
mysql数据库=>对应的factoryName 为 MySqlDBA 同时也要将那个连接字符串改成mysql数据库使用的连接字符串
sqlite数据库=>对应的factoryName 为 SQLiteDBA 同时也要将那个连接字符串改成sqlite数据库使用的连接字符串

<!--sql server数据库连接-->
    <!--<add name="connStr" connectionString="data source=www.cosen168.com;initial catalog=cosen;user id=sa;password=cosen" />-->
    <!--mysql数据库连接字符串-->
    <!--<add name="connStr" connectionString="server=localhost;initial catalog=cosen;user id=root;password=" />-->
    <!--oracle数据库连接字符串-->
    <!--<add name="connStr" connectionString="Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=192.168.0.102)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)(SID=orcl)));User Id=yttest;Password=yttest;" />-->
    <!--sqlite数据库连接字符串-->
    <add name="connStr" connectionString="Data Source=C:\Users\Administrator\Desktop\sqlite\test.db" />
    
    
    <appSettings>
      <add key="factoryName" value="SQLiteDBA" />
    </appSettings>
