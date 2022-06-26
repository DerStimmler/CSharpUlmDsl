using System;
using System.Collections.Generic;
using CSharpUlmDsl.Models;

namespace CSharpUlmDsl.Tests;

public static class ResponseMocks
{
  public const string InboxXml = @"<?xml version='1.0' encoding='utf-8'?>
<feed xmlns=""http://www.w3.org/2005/Atom"">
<author> <name>max.mustermann</name> </author>
<title>Mails für max.mustermann@ulm-dsl.de</title>
<id>urn:uuid:c0cfbe9e-bc42-45cf-928f-7521b8b482ee</id>
<updated>2022-06-25T19:29:50+02:00</updated>
<entry>
<title>Reihenendhaus Hilden: Wunschpreis noch nicht geprüft</title>
<link href=""https://ulm-dsl.de/details.php?search=max.mustermann&amp;nr=5267""/>
<id>5267</id>
<updated>Wed, 22 Jun 2022 14:15:43 +0000</updated><summary><![CDATA[ subject => =?utf-8?Q?Reihenendhaus=20Hilden=3A=20Wunschpreis=20noch=20nicht=20gepr=C3=BCft?=
from => =?utf-8?Q?Fabian=20Wedeking=20|=20Homeday=20|=20Hilden?= <kontakt@homeday.de>
to => =?utf-8?Q?Herr=20Mustermann?= <max.mustermann@ulm-dsl.de>
date => Wed, 22 Jun 2022 14:15:43 +0000
message_id => <d2c09ae678fc57d67cd43f1f2.53e73396dd.20220622141531.81f813ca4c.3fc61014@mail236.suw18.rsgsv.net>
size => 110231
uid => 3047895620
msgno => 5267
recent => 0
flagged => 0
answered => 0
deleted => 0
seen => 1
draft => 0
udate => 1655907353
]]></summary>
<content><![CDATA[ subject => =?utf-8?Q?Reihenendhaus=20Hilden=3A=20Wunschpreis=20noch=20nicht=20gepr=C3=BCft?=
from => =?utf-8?Q?Fabian=20Wedeking=20|=20Homeday=20|=20Hilden?= <kontakt@homeday.de>
to => =?utf-8?Q?Herr=20Mustermann?= <max.mustermann@ulm-dsl.de>
date => Wed, 22 Jun 2022 14:15:43 +0000
message_id => <d2c09ae678fc57d67cd43f1f2.53e73396dd.20220622141531.81f813ca4c.3fc61014@mail236.suw18.rsgsv.net>
size => 110231
uid => 3047895620
msgno => 5267
recent => 0
flagged => 0
answered => 0
deleted => 0
seen => 1
draft => 0
udate => 1655907353
]]></content>
</entry>
<entry>
<title>AW: Lindenstraße 12C: Wie viel müsste man Ihnen bieten, damit Sie verkaufen?</title>
<link href=""https://ulm-dsl.de/details.php?search=max.mustermann&amp;nr=4305""/>
<id>4305</id>
<updated>Mon, 20 Jun 2022 15:50:08 +0000</updated><summary><![CDATA[ subject => =?utf-8?Q?AW=3A=20Lindenstra=C3=9Fe=2012C=3A=20Wie=20viel=20m=C3=BCsste=20man=20Ihnen=20bieten=2C=20damit=20Sie=20verkaufen=3F?=
from => =?utf-8?Q?Fabian=20Wedeking=20|=20Homeday=20|=20Hilden?= <kontakt@homeday.de>
to => =?utf-8?Q?Herr=20Mustermann?= <max.mustermann@ulm-dsl.de>
date => Mon, 20 Jun 2022 15:50:08 +0000
message_id => <d2c09ae678fc57d67cd43f1f2.53e73396dd.20220620154957.9c362a3d63.c5b7573b@mail110.sea41.rsgsv.net>
size => 110370
uid => 3047894568
msgno => 4305
recent => 0
flagged => 0
answered => 0
deleted => 0
seen => 1
draft => 0
udate => 1655740217
]]></summary>
<content><![CDATA[ subject => =?utf-8?Q?AW=3A=20Lindenstra=C3=9Fe=2012C=3A=20Wie=20viel=20m=C3=BCsste=20man=20Ihnen=20bieten=2C=20damit=20Sie=20verkaufen=3F?=
from => =?utf-8?Q?Fabian=20Wedeking=20|=20Homeday=20|=20Hilden?= <kontakt@homeday.de>
to => =?utf-8?Q?Herr=20Mustermann?= <max.mustermann@ulm-dsl.de>
date => Mon, 20 Jun 2022 15:50:08 +0000
message_id => <d2c09ae678fc57d67cd43f1f2.53e73396dd.20220620154957.9c362a3d63.c5b7573b@mail110.sea41.rsgsv.net>
size => 110370
uid => 3047894568
msgno => 4305
recent => 0
flagged => 0
answered => 0
deleted => 0
seen => 1
draft => 0
udate => 1655740217
]]></content>
</entry>

</feed>";

  public const string EmptyInboxXml = @"<?xml version='1.0' encoding='utf-8'?>
<feed xmlns=""http://www.w3.org/2005/Atom"">
  <author> <name>stimmler</name> </author>
  <title>Mails für stimmler@ulm-dsl.de</title>
  <id>urn:uuid:c0cfbe9e-bc42-45cf-928f-7521b8b482ee</id>
  <updated>2022-06-25T20:57:47+02:00</updated>
  <entry>
  <title>0 E-Mails im Posteingang</title>
  <link href=""http://ulm-dsl.de/inbox-api.php?name=stimmler""/>
    <id>urn:uuid:e16f48ad-844b-4999-9d8b-a71f859a33a2</id>
  <updated>2022-06-25T20:57:47+02:00</updated>
  <summary>Keine E-Mails für <b>stimmler@ulm-dsl.de</b> im Posteingang</summary>
  <content>Evtl. sind Ihre E-Mails noch nicht im Posteingang angekommen.
    Bitte versuchen Sie es in wenigen Minuten noch einmal.</content>
  </entry>
  </feed>";

  public const string SingleMail5267Xml = @"<?xml version='1.0' encoding='utf-8'?>
<feed xmlns=""http://www.w3.org/2005/Atom"">
  <author>
  <name>max.mustermann</name>
  </author>
  <title>Mail 5267 an max.mustermann@ulm-dsl.de</title>
  <id>urn:uuid:c0cfbe9e-bc42-45cf-928f-7521b8b482ee</id>
  <updated>2022-06-25T19:39:24+02:00</updated>
  <entry>
  <title>Reihenendhaus Hilden: Wunschpreis noch nicht geprüft</title>
  <link href=""https://ulm-dsl.de/details.php?search=max.mustermann&amp;nr=5267"" />
    <id>5267</id>
  <updated>Wed, 22 Jun 2022 14:15:43 +0000</updated>
  <summary>
  <![CDATA[<html>
  <head>
  <style type=""text/css"">body {background-color: red;}</style></head>
  <body>Texttext</body>
  </html><br><br>]]>
  </summary>
  <content>
  <![CDATA[<html><body>Testtext</body></html>]]>
  </content>
  </entry>
  </feed>";

  public const string SingleMail4305Xml = @"<?xml version='1.0' encoding='utf-8'?>
<feed xmlns=""http://www.w3.org/2005/Atom"">
  <author>
  <name>max.mustermann</name>
  </author>
  <title>Mail 4305 an max.mustermann@ulm-dsl.de</title>
  <id>urn:uuid:c0cfbe9e-bc42-45cf-928f-7521b8b482ee</id>
  <updated>2022-06-25T19:43:30+02:00</updated>
  <entry>
  <title>AW: Lindenstraße 12C: Wie viel müsste man Ihnen bieten, damit Sie verkaufen?</title>
  <link href=""https://ulm-dsl.de/details.php?search=max.mustermann&amp;nr=4305"" />
    <id>4305</id>
  <updated>Mon, 20 Jun 2022 15:50:08 +0000</updated>
  <summary>
  <![CDATA[ Testtext2]]>
  </summary>
  <content>
  <![CDATA[ Testtext2]]>
  </content>
  </entry>
  </feed>";

  public const string InvalidId4 = @"<?xml version='1.0' encoding='utf-8'?>
<feed xmlns=""http://www.w3.org/2005/Atom"">
  <author> <name>max.mustermann</name> </author>
  <title>Mail 4 an max.mustermann@ulm-dsl.de</title>
  <id>urn:uuid:c0cfbe9e-bc42-45cf-928f-7521b8b482ee</id>
  <updated>2022-06-25T20:49:03+02:00</updated>
  <entry>
  <title>E-Mail nicht gefunden</title>
  <link href=""https://ulm-dsl.de/mail-api.php?name=max.mustermann&amp;id=4""/>
    <id>4</id>
  <updated>2022-06-25T20:49:03+02:00</updated>
  <summary><![CDATA[ Keine E-Mail mit der Nummer 4 an <b>max.mustermann@ulm-dsl.de</b> im Posteingang]]></summary>
  <content>Evtl. ist Ihre E-Mail noch nicht im Posteingang angekommen.
    Bitte versuchen Sie es in wenigen Minuten noch einmal.</content>
  </entry>
  </feed>";

  public static UlmDslMail SingleMail5267 = new()
  {
    Id = 5267,
    Date = new DateTimeOffset(2022, 6, 22, 14, 15, 43, TimeSpan.Zero),
    Link = new Uri("https://ulm-dsl.de/details.php?search=max.mustermann&nr=5267"),
    Subject = "Reihenendhaus Hilden: Wunschpreis noch nicht geprüft",
    Recipient = new UlmDslMailRecipient { DisplayName = "Herr Mustermann", Email = "max.mustermann@ulm-dsl.de" },
    Sender = new UlmDslMailSender
    {
      DisplayName = "Fabian Wedeking | Homeday | Hilden",
      Email = "kontakt@homeday.de"
    },
    Body = @"<html><body>Testtext</body></html>"
  };

  public static UlmDslMail SingleMail43056 = new()
  {
    Id = 4305,
    Date = new DateTimeOffset(2022, 6, 20, 15, 50, 8, TimeSpan.Zero),
    Link = new Uri("https://ulm-dsl.de/details.php?search=max.mustermann&nr=4305"),
    Subject = "AW: Lindenstraße 12C: Wie viel müsste man Ihnen bieten, damit Sie verkaufen?",
    Recipient = new UlmDslMailRecipient { DisplayName = "Herr Mustermann", Email = "max.mustermann@ulm-dsl.de" },
    Sender = new UlmDslMailSender
    {
      DisplayName = "Fabian Wedeking | Homeday | Hilden",
      Email = "kontakt@homeday.de"
    },
    Body = @"Testtext2"
  };

  public static List<UlmDslMailBasicInfo> Inbox = new() { SingleMail5267, SingleMail43056 };
}
