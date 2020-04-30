-- phpMyAdmin SQL Dump
-- version 4.8.2
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Erstellungszeit: 30. Apr 2020 um 12:36
-- Server-Version: 10.1.34-MariaDB
-- PHP-Version: 7.2.8

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Datenbank: `erm_bewerbungen`
--

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `abteilung`
--

CREATE TABLE `abteilung` (
  `id` int(10) NOT NULL,
  `fk_beruf` int(10) NOT NULL,
  `fk_firma` int(10) NOT NULL,
  `fk_ansprechpartner` int(10) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Daten für Tabelle `abteilung`
--

INSERT INTO `abteilung` (`id`, `fk_beruf`, `fk_firma`, `fk_ansprechpartner`) VALUES
(1, 1, 1, 1);

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `adresse`
--

CREATE TABLE `adresse` (
  `id` int(10) NOT NULL,
  `ort` varchar(255) NOT NULL,
  `postleitzahl` int(10) NOT NULL,
  `straße` varchar(255) NOT NULL,
  `hausnummer` varchar(10) NOT NULL,
  `land` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Daten für Tabelle `adresse`
--

INSERT INTO `adresse` (`id`, `ort`, `postleitzahl`, `straße`, `hausnummer`, `land`) VALUES
(1, 'Husum', 25813, 'Theodor-Schäfer-Straße', '14-26', 'Deutschland'),
(2, 'Hamburg', 20095, 'Musterstraße', '34a', 'Deutschland'),
(3, '333', 444, '555', '666', '777'),
(4, 'Berlin', 10115, 'Berliner Straße', '23', 'Deutschland'),
(5, 'PLF', 98766, 'Wald (irgendwo)', '1', 'Japan');

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `ansprechpartner`
--

CREATE TABLE `ansprechpartner` (
  `id` int(10) NOT NULL,
  `vorname` varchar(255) NOT NULL,
  `name` varchar(255) NOT NULL,
  `telefon` varchar(50) NOT NULL,
  `e_mail` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Daten für Tabelle `ansprechpartner`
--

INSERT INTO `ansprechpartner` (`id`, `vorname`, `name`, `telefon`, `e_mail`) VALUES
(1, 'Ansprech', 'Partner', '666888', 'ansprech@partner.de');

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `ausbilder`
--

CREATE TABLE `ausbilder` (
  `id` int(10) NOT NULL,
  `vorname` varchar(255) NOT NULL,
  `name` varchar(255) NOT NULL,
  `telefon` varchar(50) NOT NULL,
  `e_mail` varchar(255) NOT NULL,
  `fk_nutzer` int(10) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Daten für Tabelle `ausbilder`
--

INSERT INTO `ausbilder` (`id`, `vorname`, `name`, `telefon`, `e_mail`, `fk_nutzer`) VALUES
(1, 'Christoph', 'Strunk', '01234-5678', 'ausbilder@tsbw.de', 2),
(2, 'Inken', 'Kotulla', '123', 'inken@kotulla.de', 7);

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `beruf`
--

CREATE TABLE `beruf` (
  `id` int(10) NOT NULL,
  `bezeichnung` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Daten für Tabelle `beruf`
--

INSERT INTO `beruf` (`id`, `bezeichnung`) VALUES
(1, 'Anwendungsentwicklung'),
(2, 'Systemadministrator');

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `bewerbung`
--

CREATE TABLE `bewerbung` (
  `id` int(10) NOT NULL,
  `gesendet_datum` date NOT NULL,
  `antwort_datum` date DEFAULT NULL,
  `fk_bewerbung_typ` int(10) NOT NULL,
  `fk_bewerbung_status` int(10) NOT NULL,
  `fk_teilnehmer` int(10) NOT NULL,
  `fk_abteilung` int(10) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Daten für Tabelle `bewerbung`
--

INSERT INTO `bewerbung` (`id`, `gesendet_datum`, `antwort_datum`, `fk_bewerbung_typ`, `fk_bewerbung_status`, `fk_teilnehmer`, `fk_abteilung`) VALUES
(1, '2020-02-01', NULL, 1, 1, 1, 1);

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `bewerbung_status`
--

CREATE TABLE `bewerbung_status` (
  `id` int(10) NOT NULL,
  `beschreibung` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Daten für Tabelle `bewerbung_status`
--

INSERT INTO `bewerbung_status` (`id`, `beschreibung`) VALUES
(1, 'offen');

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `bewerbung_typ`
--

CREATE TABLE `bewerbung_typ` (
  `id` int(10) NOT NULL,
  `beschreibung` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Daten für Tabelle `bewerbung_typ`
--

INSERT INTO `bewerbung_typ` (`id`, `beschreibung`) VALUES
(1, 'Initiativbewerbung');

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `firma`
--

CREATE TABLE `firma` (
  `id` int(10) NOT NULL,
  `name` varchar(255) NOT NULL,
  `bewerbung_telefon` int(50) NOT NULL,
  `bewerbung_e_mail` varchar(255) DEFAULT NULL,
  `fk_adresse` int(10) NOT NULL,
  `beschreibung` text
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Daten für Tabelle `firma`
--

INSERT INTO `firma` (`id`, `name`, `bewerbung_telefon`, `bewerbung_e_mail`, `fk_adresse`, `beschreibung`) VALUES
(1, 'Alles Ag', 332211, 'muster@ag.de', 2, 'Eine Beispielfirmas');

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `nutzer`
--

CREATE TABLE `nutzer` (
  `id` int(10) NOT NULL,
  `passwort` varchar(255) NOT NULL,
  `fk_sicherheitsabfrage` int(10) NOT NULL,
  `fk_nutzertyp` int(10) NOT NULL,
  `sicherheitsantwort` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Daten für Tabelle `nutzer`
--

INSERT INTO `nutzer` (`id`, `passwort`, `fk_sicherheitsabfrage`, `fk_nutzertyp`, `sicherheitsantwort`) VALUES
(1, 'Eigentor', 4, 2, 'Michael Schuhmacher'),
(2, 'Tobi', 1, 1, 'Knödel'),
(7, 'neue', 1, 1, 'eis'),
(9, '888', 2, 2, '999'),
(11, 'Stiefel', 4, 2, 'Otto'),
(12, 'Izuku', 4, 2, 'Blut');

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `nutzertyp`
--

CREATE TABLE `nutzertyp` (
  `id` int(10) NOT NULL,
  `nutzertyp` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Daten für Tabelle `nutzertyp`
--

INSERT INTO `nutzertyp` (`id`, `nutzertyp`) VALUES
(1, 'Ausbilder'),
(2, 'Teilnehmer');

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `sicherheitsfrage`
--

CREATE TABLE `sicherheitsfrage` (
  `id` int(10) NOT NULL,
  `frage` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Daten für Tabelle `sicherheitsfrage`
--

INSERT INTO `sicherheitsfrage` (`id`, `frage`) VALUES
(1, 'Was ist mein Lieblingsessen?'),
(2, 'Wie hieß mein erstes Haustier?'),
(3, 'Was ist mein Lieblingsfilm?'),
(4, 'Wer ist mein Kindheitsheld?');

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `teilnehmer`
--

CREATE TABLE `teilnehmer` (
  `id` int(10) NOT NULL,
  `vorname` varchar(255) NOT NULL,
  `name` varchar(255) NOT NULL,
  `telefon` varchar(50) NOT NULL,
  `e_mail` varchar(255) NOT NULL,
  `fk_beruf` int(10) DEFAULT NULL,
  `fk_ausbilder` int(10) NOT NULL,
  `fk_adresse` int(10) NOT NULL,
  `fk_nutzer` int(10) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Daten für Tabelle `teilnehmer`
--

INSERT INTO `teilnehmer` (`id`, `vorname`, `name`, `telefon`, `e_mail`, `fk_beruf`, `fk_ausbilder`, `fk_adresse`, `fk_nutzer`) VALUES
(1, 'Lars Rune', 'Christiansen', '04681-11125', 'ist@geheim.de', 2, 1, 1, 1),
(3, 'Max', 'Mustermann', '04681-22913', 'max@mustermann.de', 1, 1, 4, 11),
(4, 'Himiko', 'Toga', 'xxxxxx', 'mad@stabber.de', 2, 2, 5, 12);

--
-- Indizes der exportierten Tabellen
--

--
-- Indizes für die Tabelle `abteilung`
--
ALTER TABLE `abteilung`
  ADD PRIMARY KEY (`id`),
  ADD KEY `fk_beruf` (`fk_beruf`),
  ADD KEY `fk_firma` (`fk_firma`),
  ADD KEY `fk_ansprechpartner` (`fk_ansprechpartner`);

--
-- Indizes für die Tabelle `adresse`
--
ALTER TABLE `adresse`
  ADD PRIMARY KEY (`id`);

--
-- Indizes für die Tabelle `ansprechpartner`
--
ALTER TABLE `ansprechpartner`
  ADD PRIMARY KEY (`id`);

--
-- Indizes für die Tabelle `ausbilder`
--
ALTER TABLE `ausbilder`
  ADD PRIMARY KEY (`id`),
  ADD KEY `fk_nutzer` (`fk_nutzer`);

--
-- Indizes für die Tabelle `beruf`
--
ALTER TABLE `beruf`
  ADD PRIMARY KEY (`id`);

--
-- Indizes für die Tabelle `bewerbung`
--
ALTER TABLE `bewerbung`
  ADD PRIMARY KEY (`id`),
  ADD KEY `fk_bewerbung_typ` (`fk_bewerbung_typ`),
  ADD KEY `fk_bewerbung_status` (`fk_bewerbung_status`),
  ADD KEY `fk_teilnehmer` (`fk_teilnehmer`),
  ADD KEY `fk_abteilung` (`fk_abteilung`);

--
-- Indizes für die Tabelle `bewerbung_status`
--
ALTER TABLE `bewerbung_status`
  ADD PRIMARY KEY (`id`);

--
-- Indizes für die Tabelle `bewerbung_typ`
--
ALTER TABLE `bewerbung_typ`
  ADD PRIMARY KEY (`id`);

--
-- Indizes für die Tabelle `firma`
--
ALTER TABLE `firma`
  ADD PRIMARY KEY (`id`),
  ADD KEY `fk_adresse` (`fk_adresse`);

--
-- Indizes für die Tabelle `nutzer`
--
ALTER TABLE `nutzer`
  ADD PRIMARY KEY (`id`),
  ADD KEY `fk_nutzertyp` (`fk_nutzertyp`),
  ADD KEY `fk_sicherheitsabfrage` (`fk_sicherheitsabfrage`);

--
-- Indizes für die Tabelle `nutzertyp`
--
ALTER TABLE `nutzertyp`
  ADD PRIMARY KEY (`id`);

--
-- Indizes für die Tabelle `sicherheitsfrage`
--
ALTER TABLE `sicherheitsfrage`
  ADD PRIMARY KEY (`id`);

--
-- Indizes für die Tabelle `teilnehmer`
--
ALTER TABLE `teilnehmer`
  ADD PRIMARY KEY (`id`),
  ADD KEY `fk_beruf` (`fk_beruf`),
  ADD KEY `fk_ausbilder` (`fk_ausbilder`),
  ADD KEY `fk_adresse` (`fk_adresse`),
  ADD KEY `fk_nutzer` (`fk_nutzer`);

--
-- AUTO_INCREMENT für exportierte Tabellen
--

--
-- AUTO_INCREMENT für Tabelle `abteilung`
--
ALTER TABLE `abteilung`
  MODIFY `id` int(10) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT für Tabelle `adresse`
--
ALTER TABLE `adresse`
  MODIFY `id` int(10) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT für Tabelle `ansprechpartner`
--
ALTER TABLE `ansprechpartner`
  MODIFY `id` int(10) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT für Tabelle `ausbilder`
--
ALTER TABLE `ausbilder`
  MODIFY `id` int(10) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT für Tabelle `beruf`
--
ALTER TABLE `beruf`
  MODIFY `id` int(10) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT für Tabelle `bewerbung`
--
ALTER TABLE `bewerbung`
  MODIFY `id` int(10) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT für Tabelle `bewerbung_status`
--
ALTER TABLE `bewerbung_status`
  MODIFY `id` int(10) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT für Tabelle `bewerbung_typ`
--
ALTER TABLE `bewerbung_typ`
  MODIFY `id` int(10) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT für Tabelle `firma`
--
ALTER TABLE `firma`
  MODIFY `id` int(10) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT für Tabelle `nutzer`
--
ALTER TABLE `nutzer`
  MODIFY `id` int(10) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=13;

--
-- AUTO_INCREMENT für Tabelle `nutzertyp`
--
ALTER TABLE `nutzertyp`
  MODIFY `id` int(10) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT für Tabelle `sicherheitsfrage`
--
ALTER TABLE `sicherheitsfrage`
  MODIFY `id` int(10) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT für Tabelle `teilnehmer`
--
ALTER TABLE `teilnehmer`
  MODIFY `id` int(10) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- Constraints der exportierten Tabellen
--

--
-- Constraints der Tabelle `abteilung`
--
ALTER TABLE `abteilung`
  ADD CONSTRAINT `abteilung_ibfk_1` FOREIGN KEY (`fk_ansprechpartner`) REFERENCES `ansprechpartner` (`id`),
  ADD CONSTRAINT `abteilung_ibfk_2` FOREIGN KEY (`fk_beruf`) REFERENCES `beruf` (`id`),
  ADD CONSTRAINT `abteilung_ibfk_3` FOREIGN KEY (`fk_firma`) REFERENCES `firma` (`id`);

--
-- Constraints der Tabelle `ausbilder`
--
ALTER TABLE `ausbilder`
  ADD CONSTRAINT `ausbilder_ibfk_1` FOREIGN KEY (`fk_nutzer`) REFERENCES `nutzer` (`id`);

--
-- Constraints der Tabelle `bewerbung`
--
ALTER TABLE `bewerbung`
  ADD CONSTRAINT `bewerbung_ibfk_1` FOREIGN KEY (`fk_abteilung`) REFERENCES `abteilung` (`id`),
  ADD CONSTRAINT `bewerbung_ibfk_2` FOREIGN KEY (`fk_bewerbung_status`) REFERENCES `bewerbung_status` (`id`),
  ADD CONSTRAINT `bewerbung_ibfk_3` FOREIGN KEY (`fk_bewerbung_typ`) REFERENCES `bewerbung_typ` (`id`),
  ADD CONSTRAINT `bewerbung_ibfk_4` FOREIGN KEY (`fk_teilnehmer`) REFERENCES `teilnehmer` (`id`);

--
-- Constraints der Tabelle `firma`
--
ALTER TABLE `firma`
  ADD CONSTRAINT `firma_ibfk_1` FOREIGN KEY (`fk_adresse`) REFERENCES `adresse` (`id`);

--
-- Constraints der Tabelle `nutzer`
--
ALTER TABLE `nutzer`
  ADD CONSTRAINT `nutzer_ibfk_1` FOREIGN KEY (`fk_nutzertyp`) REFERENCES `nutzertyp` (`id`),
  ADD CONSTRAINT `nutzer_ibfk_2` FOREIGN KEY (`fk_sicherheitsabfrage`) REFERENCES `sicherheitsfrage` (`id`);

--
-- Constraints der Tabelle `teilnehmer`
--
ALTER TABLE `teilnehmer`
  ADD CONSTRAINT `teilnehmer_ibfk_1` FOREIGN KEY (`fk_adresse`) REFERENCES `adresse` (`id`),
  ADD CONSTRAINT `teilnehmer_ibfk_2` FOREIGN KEY (`fk_ausbilder`) REFERENCES `ausbilder` (`id`),
  ADD CONSTRAINT `teilnehmer_ibfk_3` FOREIGN KEY (`fk_beruf`) REFERENCES `beruf` (`id`),
  ADD CONSTRAINT `teilnehmer_ibfk_4` FOREIGN KEY (`fk_nutzer`) REFERENCES `nutzer` (`id`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
