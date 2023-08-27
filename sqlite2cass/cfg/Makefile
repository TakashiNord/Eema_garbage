include /root/LinuxRT/RSDU5_RT_5.23.1.0_20230330_145720_RTConfig/Scripts/admin_scripts/usr/share/ema/etc/ema/rsdu_config_defines.inc

SUBDIRS_LIBS += Libs/libuuid/Makefile
SUBDIRS_LIBS += Libs/libamqpcpp/Makefile
SUBDIRS_LIBS += Libs/rsdurmq/Makefile
SUBDIRS_LIBS += Libs/utils/linux/Makefile
SUBDIRS_LIBS += Libs/rsduss/linux/Makefile
SUBDIRS_LIBS += Libs/RSDUCrypto/linux/Makefile
SUBDIRS_LIBS += Libs/serial/linux/Makefile
SUBDIRS_LIBS += Libs/IEC60870/linux/Makefile
SUBDIRS_LIBS += Libs/CalcLib/linux/Makefile
SUBDIRS_LIBS += Libs/sqlite3/Makefile
SUBDIRS_LIBS += Libs/librabbitmq/Makefile
SUBDIRS_LIBS += Libs/curl/Makefile
SUBDIRS_LIBS += Libs/libmodbus/Makefile
SUBDIRS_LIBS += Libs/tekon/Makefile
SUBDIRS_LIBS += Libs/lua/Makefile
SUBDIRS_LIBS += Libs/rsducass/Makefile
SUBDIRS_LIBS += Libs/libiec61850/Makefile
SUBDIRS_LIBS += Libs/libtase2/Makefile

SUBDIRS_OTHERS += Others/FileGen_Linux/linux/Makefile
SUBDIRS_OTHERS += Others/mbadmin/linux/Makefile
SUBDIRS_OTHERS += Others/inpsendsign/linux/Makefile
SUBDIRS_OTHERS += Others/sgtest/linux/Makefile
SUBDIRS_OTHERS += Others/sqltest/linux/Makefile
SUBDIRS_OTHERS += Others/sysutil/linux/Makefile
SUBDIRS_OTHERS += Others/rtquery/linux/Makefile
SUBDIRS_OTHERS += Others/rtsetcontr/linux/Makefile
SUBDIRS_OTHERS += Others/DebugManage/linux/Makefile
#SUBDIRS_OTHERS += Others/cid2profile/linux/Makefile
SUBDIRS_OTHERS += Others/oracle2cass/linux/Makefile
SUBDIRS_OTHERS += Others/oracle2cass/linux/Makefile.sqlite2cass

SUBDIRS_SERVERS += Servers/smagent/linux/Makefile
SUBDIRS_SERVERS += Servers/signal3/linux/Makefile
SUBDIRS_SERVERS += Servers/Bridge/linux/Makefile
SUBDIRS_SERVERS += Servers/ac_serv/linux/Makefile.acserv
SUBDIRS_SERVERS += Servers/ac_serv/linux/Makefile.acservlight
SUBDIRS_SERVERS += Servers/ArcWrite/linux/Makefile
SUBDIRS_SERVERS += Servers/ArcWriteLight/linux/Makefile
SUBDIRS_SERVERS += Servers/ArcRead/linux/Makefile
SUBDIRS_SERVERS += Servers/ArcRestore/linux/Makefile
SUBDIRS_SERVERS += Servers/pwswitch/linux/Makefile.automat
SUBDIRS_SERVERS += Servers/ai_rpl/linux/Makefile
SUBDIRS_SERVERS += Servers/ai_wostoic/linux/Makefile
SUBDIRS_SERVERS += Servers/calcd/linux/Makefile
SUBDIRS_SERVERS += Servers/DataSrv/linux/Makefile
#SUBDIRS_SERVERS += Servers/dbrtsrv/linux/Makefile
SUBDIRS_SERVERS += Servers/DCS/linux/Makefile
#SUBDIRS_SERVERS += Servers/dsgw/linux/Makefile
SUBDIRS_SERVERS += Servers/dpload/linux/Makefile
SUBDIRS_SERVERS += Servers/Oictech/linux/Makefile.elreg
SUBDIRS_SERVERS += Servers/Enmacsrv/linux/Makefile
SUBDIRS_SERVERS += Servers/fdstsrv/linux/Makefile
SUBDIRS_SERVERS += Servers/graph50/linux/Makefile
SUBDIRS_SERVERS += Servers/layout/linux/Makefile
SUBDIRS_SERVERS += Servers/loutadcp/linux/Makefile
SUBDIRS_SERVERS += Servers/oicdcp/linux/Makefile
SUBDIRS_SERVERS += Servers/Oictech/linux/Makefile.phreg
SUBDIRS_SERVERS += Servers/pwswitch/linux/Makefile.pwswitch
SUBDIRS_SERVERS += Servers/rdarch/linux/Makefile
SUBDIRS_SERVERS += Servers/rtarch/linux/Makefile
#SUBDIRS_SERVERS += Servers/rtqueryd/linux/Makefile
SUBDIRS_SERVERS += Servers/sakura/linux/Makefile
SUBDIRS_SERVERS += Servers/schit40/linux/Makefile
SUBDIRS_SERVERS += Servers/signal_bridge/linux/Makefile
SUBDIRS_SERVERS += Servers/tmdap/linux/Makefile
SUBDIRS_SERVERS += Servers/tmsrvd/linux/Makefile
SUBDIRS_SERVERS += Servers/sysmon/linux/Makefile
SUBDIRS_SERVERS += Servers/iccp/linux/Makefile
SUBDIRS_SERVERS += Servers/topology/linux/Makefile


PRODUCT_NAME=$(RCD_PRODUCT_NAME)
TOPDIR = $(RCD_RSDU_TOPDIR)
PREFIX = $(RCD_PREFIX)
PREFIX_BIN = $(RCD_PREFIX_BIN)
PREFIX_LIB = $(RCD_PREFIX_LIB)
PREFIX_INC = $(RCD_PREFIX_INC)
HW_PLATFORM = $(RCD_ARCH)
RSDU_CONFIG_FILE = $(RCD_RSDU_CONFIG_FILENAME)
USE_RSDUENV = $(RCD_USE_RSDUENV)
CFG_ORACLE_BASE = $(RCD_ORACLE_BASE_PATH)
CFG_ORACLE_HOME = $(RCD_ORACLE_HOME_PATH)
CFG_ORACLE_TNS_ADMIN = $(RCD_ORACLE_TNS_ADMIN_PATH)
INST_EMA_SCRIPT = $(RCD_INSTALL_EMA_SCRIPT)
INST_EMA_AUTOLOAD_SCRIPT = $(RCD_INSTALL_EMA_AUTOLOAD_SCRIPT)
INST_EMA_CFG_FILE = $(RCD_INSTALL_EMA_CFG_FILE)
INST_HOST_INI_FILE = $(RCD_INSTALL_HOST_INI_FILE)
INST_EMA_TIME_SCRIPT = $(RCD_INSTALL_EMATIME_SCRIPT)
INST_RSDUENV_SCRIPT = $(RCD_INSTALL_RSDUENV_SCRIPT)
UPDATE_SYSCLT_CONF = $(RCD_UPDATE_SYSCTL_CONF)
INSTALL_LOGROTATE = $(RCD_INSTALL_LOGROTATE)
WAIT_DB_DELAY = $(RCD_WAIT_DB_DELAY)
DB_TYPE = $(RCD_DB_TYPE)
DB_HOST = $(RCD_DB_HOST)
CONFIG_NAME = $(RCD_CONFIG_NAME)
CONFIG_DESC = $(RCD_CONFIG_DESC)
VERSION_FILENAME = $(RCD_VERSION_FILENAME)
PRODUCT_VERSION = $(RCD_PRODUCT_VERSION)


DIR_DIST = .dist
SCRIPTS_PATH=$(TOPDIR)/Scripts

################################################################################
# Echo colors defines
################################################################################

ECHO = echo
ECHO_ESC = $(ECHO) -e

ESC = \033

BGCL_BLACK = 40
BGCL_RED = 41
BGCL_GREEN = 42
BGCL_YELLOW = 43
BGCL_BLUE = 44
BGCL_MAGENTA = 45
BGCL_CYAN = 46
BGCL_WHITE = 47

TCL_BLACK = 30
TCL_RED = 31
TCL_GREEN = 32
TCL_YELLOW = 33
TCL_BLUE = 34
TCL_MAGENTA = 35
TCL_CYAN = 36
TCL_WHITE = 37

EBOLD_ON = $(ESC)[1m
EBOLD_OFF = $(ESC)[0m

EUL_ON = $(ESC)[4m
EUL_OFF = $(ESC)[0m


ETCL_BLACK = $(ESC)[$(TCL_BLACK)m
ETCL_RED = $(ESC)[$(TCL_RED)m
ETCL_GREEN = $(ESC)[$(TCL_GREEN)m
ETCL_YELLOW = $(ESC)[$(TCL_YELLOW)m
ETCL_BLUE = $(ESC)[$(TCL_BLUE)m
ETCL_MAGENTA = $(ESC)[$(TCL_MAGENTA)m
ETCL_CYAN = $(ESC)[$(TCL_CYAN)m
ETCL_WHITE = $(ESC)[$(TCL_WHITE)m
ETCL_NORMAL = $(ECTL_WHITE)$(EBOLD_OFF)


################################################################################
# END! Echo colors defines
################################################################################

.PHONY: srcdist subdirs subdirs_bin $(SUBDIRS_LIBS) $(SUBDIRS_OTHERS) $(SUBDIRS_SERVERS) srcdist-clean help helpme .build .finished

all: TARGET = all
all: .build

build: .build

.build: TRAGET = .build
.build: .prebuild subdirs .finished

libs: $(SUBDIRS_LIBS) .finished

other: $(SUBDIRS_OTHERS) .finished

servers: $(SUBDIRS_SERVERS) .finished


## Prebuild
.prebuild:

subdirs: $(SUBDIRS_LIBS) $(SUBDIRS_OTHERS) $(SUBDIRS_SERVERS)
subdirs_bin: $(SUBDIRS_OTHERS) $(SUBDIRS_SERVERS)

$(SUBDIRS_LIBS):
	@$(ECHO) "================================================================================"
	@$(ECHO_ESC) "$(ETCL_GREEN)## $(ETCL_YELLOW)$(EBOLD_ON)Making $@$(ETCL_NORMAL)";
	@if [ -f $@ ]; then \
		prjdir=$(dir $@); \
		mfname=$(notdir $@); \
	else \
		prjdir=$@; \
		mfname=Makefile; \
	fi; \
	if [ -f $(SCRIPTS_PATH)/RSDUENV.default ]; then \
		echo "Using '$(SCRIPTS_PATH)/RSDUENV.default'"; \
		. $(SCRIPTS_PATH)/RSDUENV.default; \
	fi; \
	$(MAKE) -C $$prjdir -f $$mfname $(TARGET); \
	if [ "x$(TARGET)" = "xall" ]; then \
		$$rsduenv_path $(MAKE) -C $$prjdir -f $$mfname install; \
	fi;
	@$(ECHO_ESC) "$(ETCL_GREEN)## $(EBOLD_ON)Finished $@$(ETCL_NORMAL)"

$(SUBDIRS_OTHERS):
	@$(ECHO) "================================================================================"
	@$(ECHO_ESC) "$(ETCL_GREEN)## $(ETCL_YELLOW)$(EBOLD_ON)Making $@$(ETCL_NORMAL)";
	@if [ -f $@ ]; then \
		prjdir=$(dir $@); \
		mfname=$(notdir $@); \
	else \
		prjdir=$@; \
		mfname=Makefile; \
	fi; \
	if [ -f $(SCRIPTS_PATH)/RSDUENV.default ]; then \
		echo "Using '$(SCRIPTS_PATH)/RSDUENV.default'"; \
		. $(SCRIPTS_PATH)/RSDUENV.default; \
	fi; \
	$(MAKE) -C $$prjdir -f $$mfname $(TARGET);
	@$(ECHO_ESC) "$(ETCL_GREEN)## $(EBOLD_ON)Finished $@$(ETCL_NORMAL)"

$(SUBDIRS_SERVERS):
	@$(ECHO) "================================================================================"
	@$(ECHO_ESC) "$(ETCL_GREEN)## $(ETCL_YELLOW)$(EBOLD_ON)Making $@$(ETCL_NORMAL)";
	@if [ -f $(SCRIPTS_PATH)/RSDUENV.default ]; then \
		echo "Using '$(SCRIPTS_PATH)/RSDUENV.default'"; \
		. $(SCRIPTS_PATH)/RSDUENV.default; \
	fi; \
	if [ -f $@ ]; then \
		$(MAKE) -C $(dir $@) -f $(notdir $@) $(TARGET); \
	else \
		$(MAKE) -C $@ -f Makefile $(TARGET); \
	fi;
	@$(ECHO_ESC) "$(ETCL_GREEN)## $(EBOLD_ON)Finished $@$(ETCL_NORMAL)"

install: TARGET = install
install: EMASERV = --manual
install: prepare subdirs_bin .finished

autoinstall: TARGET = install
autoinstall: prepare subdirs_bin .finished

rebuild:
	make clean
	make all

update: TARGET = install
update: update_ema_script subdirs_bin .finished

upgrade: TARGET = upgrade
upgrade: update_ema_script subdirs_bin .finished

update_ema_script:
	cp -f ./Scripts/ema $(PREFIX_BIN)/ema

prepare:
	if [ $(HW_PLATFORM) = "x86_64" ]; then key_x64="--x86-64"; fi; \
	cd ./Scripts && ./install.sh $(EMASERV) $$key_x64 --prefix=$(PREFIX) --prefix-bin=$(PREFIX_BIN) --prefix-lib=$(PREFIX_LIB) --prefix-inc=$(PREFIX_INC) --rsdu-dist-dir=$(TOPDIR) --install-ema-script=$(INST_EMA_SCRIPT) --install-ema-autoload-script=$(INST_EMA_AUTOLOAD_SCRIPT) --install-ema-cfg-file=$(INST_EMA_CFG_FILE) --install-host-ini-file=$(INST_HOST_INI_FILE) --install-rsduenv-script=$(INST_RSDUENV_SCRIPT) --install-ema-time-script=$(INST_EMA_TIME_SCRIPT) --update-sysctl-conf=$(UPDATE_SYSCLT_CONF) --update-sysctl-conf=$(UPDATE_SYSCLT_CONF) --wait-db-delay=$(WAIT_DB_DELAY) --db-type=$(DB_TYPE)

clean: TARGET = clean
clean: subdirs .finished

.finished:
	@$(ECHO) ;
	@$(ECHO_ESC) "$(ETCL_GREEN)";
	@$(ECHO_ESC) "====================================================";
	@$(ECHO_ESC) "  $(CONFIG_NAME). ($(CONFIG_DESC))  ";
	@$(ECHO_ESC) "  Make has finished successfully.  ";
	@$(ECHO_ESC) "====================================================$(ETCL_NORMAL)";

#########################################################################
# Make rebuild elregd and phregd
reelreg:
#	$(TOPDIR)/Scripts/rebuild_elregd.sh
	@if [ -f $(SCRIPTS_PATH)/RSDUENV.default ]; then \
		echo "Using '$(SCRIPTS_PATH)/RSDUENV.default'"; \
		. $(SCRIPTS_PATH)/RSDUENV.default; \
	fi; \
	make -C$(TOPDIR)/Servers/Oictech/linux/ -fMakefile.elreg rebuild && make -C$(TOPDIR)/Servers/Oictech/linux/ -fMakefile.elreg install

rephreg:
#	$(TOPDIR)/Scripts/rebuild_phregd.sh
	@if [ -f $(SCRIPTS_PATH)/RSDUENV.default ]; then \
		echo "Using '$(SCRIPTS_PATH)/RSDUENV.default'"; \
		. $(SCRIPTS_PATH)/RSDUENV.default; \
	fi; \
	make -C$(TOPDIR)/Servers/Oictech/linux/ -fMakefile.phreg rebuild && make -C$(TOPDIR)/Servers/Oictech/linux/ -fMakefile.phreg install

#########################################################################
# Make src dist
dist:
	@$(ECHO) "================================================================================"
	@$(ECHO_ESC) "$(ETCL_GREEN)## $(ETCL_YELLOW)$(EBOLD_ON)Making source dist...$(ETCL_NORMAL)";
	if [ ! -f $(SCRIPTS_PATH)/rsduutils.inc.sh ]; then \
		echo "Error!! Can't open $(SCRIPTS_PATH)/rsduutils.inc.sh"; \
		exit 1; \
	fi;
	rm -f -r ./$(DIR_DIST)
	mkdir -p ./$(DIR_DIST)
	cp -f ./Makefile.in ./$(DIR_DIST)/
	cp -f ./configure ./$(DIR_DIST)/
	if [ "x$(PRODUCT_NAME)" = "x'RSDU5'" ]; then \
		cp -f ./rsdu5.version.txt ./$(DIR_DIST)/ ;\
	fi
	if [ "x$(PRODUCT_NAME)" = "x'iSMS'" ]; then \
		cp -f ./isms.version.txt ./$(DIR_DIST)/ ;\
	fi
	for itdistpath in $(SUBDIRS_LIBS) $(SUBDIRS_OTHERS) $(SUBDIRS_SERVERS); do \
		$(ECHO_ESC) "$(ETCL_GREEN)## $(ETCL_YELLOW)$(EBOLD_ON)Make $$itdistpath$(ETCL_NORMAL)"; \
		if [ -f $$itdistpath ]; then \
			$(ECHO_ESC) "$(ETCL_GREEN)## $(ETCL_BLUE)$(EBOLD_ON)Using $$(basename $$itdistpath)$(ETCL_NORMAL)"; \
			prjpath=$$(dirname $$itdistpath); \
			$(MAKE) -C $$(dirname $$itdistpath) -f $$(basename $$itdistpath) $@; \
			make_dist_res=$$?; \
		else \
			prjpath=$$itdistpath; \
			$(MAKE) -C $$itdistpath -f Makefile $@; \
			make_dist_res=$$?; \
		fi; \
		if [ $$make_dist_res != 0 ]; then \
			echo "Make dist error $$make_dist_res"; \
			exit 1; \
		fi; \
		if [ -d $$prjpath ]; then \
			mkdir -p ./$(DIR_DIST)/$$prjpath/; \
			cp -r -f $$prjpath/$(DIR_DIST)/* ./$(DIR_DIST)/$$prjpath/; \
			cp -r -f $$prjpath/$(DIR_DIST)/.revision ./$(DIR_DIST)/$$prjpath/; \
		fi; \
	done;
	mkdir -p ./$(DIR_DIST)/Scripts
	cp -f ./Scripts/Makefile.base ./$(DIR_DIST)/Scripts/
	cp -f ./Scripts/configure ./$(DIR_DIST)/Scripts/
#	cp -f ./Scripts/ema.template ./$(DIR_DIST)/Scripts/
#	cp -f ./Scripts/ema.cfg.default ./$(DIR_DIST)/Scripts/
#	cp -f ./Scripts/host.ini.default ./$(DIR_DIST)/Scripts/
#	cp -f ./Scripts/ema_autoload ./$(DIR_DIST)/Scripts/
#	cp -f ./Scripts/ema_time.cfg.default ./$(DIR_DIST)/Scripts/
#	cp -f ./Scripts/ema_time.sh ./$(DIR_DIST)/Scripts/
	cp -f ./Scripts/gencalcsrc.sh ./$(DIR_DIST)/Scripts/
	cp -f ./Scripts/mkcalcelph.sh ./$(DIR_DIST)/Scripts/
#	cp -f ./Scripts/mem.sh ./$(DIR_DIST)/Scripts/
	cp -f ./Scripts/mkver_lnx.sh ./$(DIR_DIST)/Scripts/
	cp -f ./Scripts/mkver_lnx2.sh ./$(DIR_DIST)/Scripts/
	cp -f ./Scripts/install.sh ./$(DIR_DIST)/Scripts/
	cp -f ./Scripts/rebuild_elregd.sh ./$(DIR_DIST)/Scripts/
	cp -f ./Scripts/rebuild_phregd.sh ./$(DIR_DIST)/Scripts/
	cp -f ./Scripts/RSDU ./$(DIR_DIST)/Scripts/
	cp -f ./Scripts/RSDUENV.template ./$(DIR_DIST)/Scripts/
	cp -f ./Scripts/rsduutils.inc.sh ./$(DIR_DIST)/Scripts/
	cp -f ./Scripts/check_libmodbus.sh ./$(DIR_DIST)/Scripts/

	mkdir -p ./$(DIR_DIST)/Scripts/admin_scripts
	mkdir -p ./$(DIR_DIST)/Scripts/admin_scripts/usr/share/ema/etc/ema
#	cp -f ./Scripts/admin_scripts/usr/share/ema/etc/ema/ema.conf ./$(DIR_DIST)/Scripts/admin_scripts/usr/share/ema/etc/ema/
	cp -f ./Scripts/admin_scripts/usr/share/ema/etc/ema/host.ini.template ./$(DIR_DIST)/Scripts/admin_scripts/usr/share/ema/etc/ema/
	cp -f ./Scripts/admin_scripts/usr/share/ema/etc/ema/ema_time.cfg.template ./$(DIR_DIST)/Scripts/admin_scripts/usr/share/ema/etc/ema/
	cp -f ./Scripts/admin_scripts/usr/share/ema/etc/ema/notify_state.sh.template ./$(DIR_DIST)/Scripts/admin_scripts/usr/share/ema/etc/ema/
	cp -f ./Scripts/admin_scripts/usr/share/ema/etc/ema/topologyd.conf.template ./$(DIR_DIST)/Scripts/admin_scripts/usr/share/ema/etc/ema/

	mkdir -p ./$(DIR_DIST)/Scripts/admin_scripts/usr/bin
	cp -f ./Scripts/admin_scripts/usr/bin/ema ./$(DIR_DIST)/Scripts/admin_scripts/usr/bin/
	cp -f ./Scripts/admin_scripts/usr/bin/emaadmin ./$(DIR_DIST)/Scripts/admin_scripts/usr/bin/
	cp -f ./Scripts/admin_scripts/usr/bin/ema_time.sh ./$(DIR_DIST)/Scripts/admin_scripts/usr/bin/
	cp -f ./Scripts/admin_scripts/usr/bin/mem.sh ./$(DIR_DIST)/Scripts/admin_scripts/usr/bin/
	cp -f ./Scripts/admin_scripts/usr/bin/ismsgetstatus ./$(DIR_DIST)/Scripts/admin_scripts/usr/bin/

	mkdir -p ./$(DIR_DIST)/Scripts/admin_scripts/usr/bin/isms
	cp -f ./Scripts/admin_scripts/usr/bin/isms/isms_backup_config.sh ./$(DIR_DIST)/Scripts/admin_scripts/usr/bin/isms/
	cp -f ./Scripts/admin_scripts/usr/bin/isms/isms_restore_config.sh ./$(DIR_DIST)/Scripts/admin_scripts/usr/bin/isms/

	cp -f ./Scripts/admin_scripts/usr/bin/isms/isms_mysql_db_export.sh ./$(DIR_DIST)/Scripts/admin_scripts/usr/bin/isms/
	cp -f ./Scripts/admin_scripts/usr/bin/isms/isms_mysql_db_import.sh ./$(DIR_DIST)/Scripts/admin_scripts/usr/bin/isms/
	cp -f ./Scripts/admin_scripts/usr/bin/isms/isms_oracle_db_export.sh ./$(DIR_DIST)/Scripts/admin_scripts/usr/bin/isms/
	cp -f ./Scripts/admin_scripts/usr/bin/isms/isms_oracle_db_import.sh ./$(DIR_DIST)/Scripts/admin_scripts/usr/bin/isms/

	mkdir -p ./$(DIR_DIST)/Scripts/admin_scripts/usr/bin/isms/oracle
	cp -f ./Scripts/admin_scripts/usr/bin/isms/oracle/02a_disable_triggers.sql ./$(DIR_DIST)/Scripts/admin_scripts/usr/bin/isms/oracle/
	cp -f ./Scripts/admin_scripts/usr/bin/isms/oracle/02_diasble_constraints.sql ./$(DIR_DIST)/Scripts/admin_scripts/usr/bin/isms/oracle/
	cp -f ./Scripts/admin_scripts/usr/bin/isms/oracle/03_clear_tables.sql ./$(DIR_DIST)/Scripts/admin_scripts/usr/bin/isms/oracle/
	cp -f ./Scripts/admin_scripts/usr/bin/isms/oracle/05a_enable_triggers.sql ./$(DIR_DIST)/Scripts/admin_scripts/usr/bin/isms/oracle/
	cp -f ./Scripts/admin_scripts/usr/bin/isms/oracle/05_enable_constraints.sql ./$(DIR_DIST)/Scripts/admin_scripts/usr/bin/isms/oracle/
	cp -f ./Scripts/admin_scripts/usr/bin/isms/oracle/06_recompile_invalid_obj.sql ./$(DIR_DIST)/Scripts/admin_scripts/usr/bin/isms/oracle/
	cp -f ./Scripts/admin_scripts/usr/bin/isms/oracle/07_fix_sequences.sql ./$(DIR_DIST)/Scripts/admin_scripts/usr/bin/isms/oracle/

	cp -f ./Scripts/admin_scripts/usr/bin/isms/isms_clean_proc_passwords.sh ./$(DIR_DIST)/Scripts/admin_scripts/usr/bin/isms/

	mkdir -p ./$(DIR_DIST)/Scripts/admin_scripts/usr/share/ema/bin
	cp -f ./Scripts/admin_scripts/usr/share/ema/bin/ini_parser.inc.sh ./$(DIR_DIST)/Scripts/admin_scripts/usr/share/ema/bin/
	cp -f ./Scripts/admin_scripts/usr/share/ema/bin/rsduutils.inc.sh ./$(DIR_DIST)/Scripts/admin_scripts/usr/share/ema/bin/
#	cp -f ./Scripts/admin_scripts/usr/share/ema/bin/ema.systemd ./$(DIR_DIST)/Scripts/admin_scripts/usr/share/ema/bin/
#	cp -f ./Scripts/admin_scripts/usr/share/ema/bin/ema.sysv ./$(DIR_DIST)/Scripts/admin_scripts/usr/share/ema/bin/
	cp -f ./Scripts/admin_scripts/usr/share/ema/bin/ema.systemd.inc.sh ./$(DIR_DIST)/Scripts/admin_scripts/usr/share/ema/bin/
	cp -f ./Scripts/admin_scripts/usr/share/ema/bin/ema.sysv.inc.sh ./$(DIR_DIST)/Scripts/admin_scripts/usr/share/ema/bin/

	mkdir -p ./$(DIR_DIST)/Scripts/admin_scripts/usr/share/ema/systemd/bin
	cp -f ./Scripts/admin_scripts/usr/share/ema/systemd/bin/gen-default-service.sh ./$(DIR_DIST)/Scripts/admin_scripts/usr/share/ema/systemd/bin/

	mkdir -p ./$(DIR_DIST)/Scripts/admin_scripts/usr/share/ema/systemd/templates
	cp -f ./Scripts/admin_scripts/usr/share/ema/systemd/templates/default.service.template ./$(DIR_DIST)/Scripts/admin_scripts/usr/share/ema/systemd/templates/
	cp -f ./Scripts/admin_scripts/usr/share/ema/systemd/templates/main.target.template ./$(DIR_DIST)/Scripts/admin_scripts/usr/share/ema/systemd/templates/
	cp -f ./Scripts/admin_scripts/usr/share/ema/systemd/templates/main.timer.template ./$(DIR_DIST)/Scripts/admin_scripts/usr/share/ema/systemd/templates/
#	cp -f ./Scripts/admin_scripts/usr/share/ema/systemd/topology.service.template ./$(DIR_DIST)/Scripts/admin_scripts/usr/share/ema/systemd/

	mkdir -p ./$(DIR_DIST)/Scripts/admin_scripts/usr/share/ema/sysv
	cp -f ./Scripts/admin_scripts/usr/share/ema/sysv/ema_autoload.template ./$(DIR_DIST)/Scripts/admin_scripts/usr/share/ema/sysv/ema_autoload.template
#	cp -f ./Scripts/admin_scripts/usr/share/ema/sysv/ema_autoload.debian.template ./$(DIR_DIST)/Scripts/admin_scripts/usr/share/ema/sysv/ema_autoload.debian.template
#	cp -f ./Scripts/admin_scripts/usr/share/ema/sysv/ema_autoload.redhat.template ./$(DIR_DIST)/Scripts/admin_scripts/usr/share/ema/sysv/ema_autoload.redhat.template

	\
	if [ -f $(RSDU_CONFIG_FILE) ]; then \
		cp $(RSDU_CONFIG_FILE) ./$(DIR_DIST)/; \
	fi; \
	mkdir -p ./$(DIR_DIST)/Include
	for ithfile in $$(find ./Include | grep -i -e"\.h\$$"); do \
		mkdir -p $$(dirname ./$(DIR_DIST)/$$ithfile); \
		cp -f $$ithfile ./$(DIR_DIST)/$$ithfile; \
	done;

	mkdir -p ./$(DIR_DIST)/config
	ln -s ../Servers/Oictech/linux/src/calc_el.cfg ./$(DIR_DIST)/config/calc_el.cfg
	ln -s ../Servers/Oictech/linux/src/calc_ph.cfg ./$(DIR_DIST)/config/calc_ph.cfg
	ln -s ../Servers/dbrtsrv/linux/cfg/odbc.ini.example ./$(DIR_DIST)/config/odbc.ini.example
	ln -s ../Servers/dbrtsrv/linux/cfg/odbcinst.ini.example ./$(DIR_DIST)/config/odbcinst.ini.example
	ln -s ../Scripts/RSDUENV.default ./$(DIR_DIST)/config/RSDUENV.default
	ln -s ../rsdu.config ./$(DIR_DIST)/config/rsdu.config

	. $(SCRIPTS_PATH)/rsduutils.inc.sh; \
	revnum=`make_revision_file $(TOPDIR)`; \
	cp -f ./.revision ./$(DIR_DIST)/; \
	if [ "x$(DB_TYPE)" = "xMySQL" ]; then distprefixname="iSMS_RT"; \
	else distprefixname="RSDU5_RT"; fi; \
	distdirname="$${distprefixname}_$(PRODUCT_VERSION)_`date +%Y%m%d_%H%M%S`"; \
	if [ -n "$(CONFIG_NAME)" ]; then distdirname=$${distdirname}_$(CONFIG_NAME); fi; \
	mv ./$(DIR_DIST) ./$$distdirname; \
	ln -s $$distdirname trunk; \
	tar -cjvf ./$$distdirname.tar.bz2 ./$$distdirname ./trunk; \
	rm -rf ./$$distdirname; \
	rm -rf ./trunk;

#	distdirname="$${distprefixname}_m$${revnum}_`date +%d-%m-%y_%H-%M-%S`"; \

dist-clean:
	for itdistpath in $(SUBDIRS_LIBS) $(SUBDIRS_OTHERS) $(SUBDIRS_SERVERS); do \
		$(ECHO) $$itdistpath; \
		if [ -f $$itdistpath ]; then \
			$(ECHO_ESC) "$(ETCL_GREEN)## $(ETCL_BLEU)$(EBOLD_ON)Using $$(basename $$itdistpath)$(ETCL_NORMAL)"; \
			prjpath=$$(dirname $$itdistpath); \
			$(MAKE) -C $$(dirname $$itdistpath) -f $$(basename $$itdistpath) $@; \
			rm -f $$itdistpath; \
		else \
			prjpath=$$itdistpath; \
			$(MAKE) -C $$itdistpath -f Makefile $@; \
			rm -f $$itdistpath/Makefile; \
		fi; \
	done;
	rm -f -r ./$(DIR_DIST)

#######################################
## Help information
help helpme:
	@$(ECHO) "Usage: make [goal]";
	@$(ECHO) "  Possible goals:";
	@$(ECHO) "  all           Execute default actions. Build all libraries, utilites and servers.";
	@$(ECHO) "  libs          Build all RSDU libraries.";
	@$(ECHO) "  others        Build all RSDU others utilites.";
	@$(ECHO) "  servers       Build all RSDU servers.";
	@$(ECHO);
	@$(ECHO) "  clean         Clean all projects.";
	@$(ECHO);
	@$(ECHO) "  dist          Make distrib archive.";
	@$(ECHO) "  dist-clean    Clean all projects distrib directory.";
	@$(ECHO);
	@$(ECHO) "  install       Install all utilites and servers. Make changes in the system files.";
	@$(ECHO) "                Install 'ema' script for auto start. Start installing in manual mode.";
	@$(ECHO) "  autoinstall   Start full instalation without any questions.";
	@$(ECHO) ;
	@$(ECHO) "  prepare       Create all necessary directories. Make changes in the system files.";
	@$(ECHO) "  update        Install utilites and servers without prepare step.";
	@$(ECHO) "  upgrade       Rebuild and install without prepare step. Same as make clean && make all && make update.";
	@$(ECHO);
	@$(ECHO) "  reelreg       Rebuild 'elregd' server for updating formulas.";
	@$(ECHO) "  rephreg       Rebuild 'phregd' server for updating formulas.";
	@$(ECHO);
	@$(ECHO) "  help, helpme  Show this help information.";
