﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GBxCart_RW {
    public partial class GB_Specify_Info : Form {

        public GB_Specify_Info() {
            InitializeComponent();
        }

        private void cartinfoapplybutton_Click(object sender, EventArgs e) {
            Form1.writeRomCartType = 0;
            if (cartTypeBox.Text == "32K Gameboy Flash Cart (Audio as WR)") {
                int gbxcartPcbVersion = Program.request_value(Form1.READ_PCB_VERSION);
                if (gbxcartPcbVersion == Form1.PCB_1_0) {
                    System.Windows.Forms.MessageBox.Show("PCB v1.0 is not supported for this function.");
                }
                else {
                    Form1.writeRomCartType = 1;
                    Form1.writeRomCartSize = 0x8000;
                    Form1.headerTokens[0] = cartTypeBox.Text;
                    Form1.headerTokens[1] = "ROM size: 32KByte (no ROM banking)";
                    Form1.headerTokens[2] = "RAM size: None";
                    Form1.headerTokens[3] = "Choose a ROM file to write";
                    Form1.headerTokens[4] = "";
                }
            }
            else if (cartTypeBox.Text == "32K Gameboy Flash Cart (Normal WR)") {
                int gbxcartPcbVersion = Program.request_value(Form1.READ_PCB_VERSION);
                int gbxcartFirmwareVersion = Program.read_firmware_version();
                if (gbxcartPcbVersion == Form1.PCB_1_0) {
                    System.Windows.Forms.MessageBox.Show("PCB v1.0 is not supported for this function.");
                }
                else {
                    if (gbxcartFirmwareVersion <= 3) {
                        System.Windows.Forms.MessageBox.Show("Please update to Firmware R4 or higher to support this function.");
                    }
                    else {
                        Form1.writeRomCartType = 3;
                        Form1.writeRomCartSize = 0x8000;
                        Form1.headerTokens[0] = cartTypeBox.Text;
                        Form1.headerTokens[1] = "ROM size: 32KByte (no ROM banking)";
                        Form1.headerTokens[2] = "RAM size: None";
                        Form1.headerTokens[3] = "Choose a ROM file to write";
                        Form1.headerTokens[4] = "";
                    }
                }
            }
            else if (cartTypeBox.Text == "2M (BV5) Gameboy Flash Cart") {
                int gbxcartPcbVersion = Program.request_value(Form1.READ_PCB_VERSION);
                int gbxcartFirmwareVersion = Program.read_firmware_version();
                if (gbxcartPcbVersion == Form1.PCB_1_0) {
                    System.Windows.Forms.MessageBox.Show("PCB v1.0 is not supported for this function.");
                }
                else {
                    if (gbxcartFirmwareVersion <= 3) {
                        System.Windows.Forms.MessageBox.Show("Please update to Firmware R4 or higher to support this function.");
                    }
                    else { 
                        Form1.writeRomCartType = 2;
                        Form1.writeRomCartSize = 0x200000;
                        Form1.headerTokens[0] = cartTypeBox.Text;
                        Form1.headerTokens[1] = "ROM size: 2MByte (128 banks)";
                        Form1.headerTokens[2] = "RAM size: 8 Kbytes";
                        Form1.headerTokens[3] = "Choose a ROM file to write";
                        Form1.headerTokens[4] = "";
                    }
                }
            }
            else if (cartTypeBox.Text == "1M (ES29LV160) Gameboy Flash Cart") {
                int gbxcartPcbVersion = Program.request_value(Form1.READ_PCB_VERSION);
                int gbxcartFirmwareVersion = Program.read_firmware_version();
                if (gbxcartPcbVersion == Form1.PCB_1_0) {
                    System.Windows.Forms.MessageBox.Show("PCB v1.0 is not supported for this function.");
                }
                else {
                    int cartVoltage = Program.read_cartridge_mode();
                    if (gbxcartFirmwareVersion <= 6) {
                        System.Windows.Forms.MessageBox.Show("Please update to Firmware R7 or higher to support this function.");
                    }
                    else if (cartVoltage == 1) {
                        System.Windows.Forms.MessageBox.Show("You must use 3.3V to program this cart. Please unplug the device, switch to 3.3V, reconnect the device, press Open Port and then use the drop down Mode selection to manually change it to GBA.");
                    }
                    else {
                        Form1.writeRomCartType = 4;
                        Form1.writeRomCartSize = 0x100000;
                        Form1.headerTokens[0] = cartTypeBox.Text;
                        Form1.headerTokens[1] = "ROM size: 1MByte (64 banks)";
                        Form1.headerTokens[2] = "RAM size: None";
                        Form1.headerTokens[3] = "Choose a ROM file to write";
                        Form1.headerTokens[4] = "";
                    }
                }
            }
            

            if (Form1.writeRomCartType == 0) {
                // ROM Size
                int rom_size = 0;
                if (romsizebox.Text == "32KByte (no ROM banking)") {
                    rom_size = 0;
                }
                else if (romsizebox.Text == "64KByte (4 banks)") {
                    rom_size = 1;
                }
                else if (romsizebox.Text == "128KByte (8 banks)") {
                    rom_size = 2;
                }
                else if (romsizebox.Text == "256KByte (16 banks)") {
                    rom_size = 3;
                }
                else if (romsizebox.Text == "512KByte (32 banks)") {
                    rom_size = 4;
                }
                else if (romsizebox.Text == "1MByte (64 banks)") {
                    rom_size = 5;
                }
                else if (romsizebox.Text == "2MByte (128 banks)") {
                    rom_size = 6;
                }
                else if (romsizebox.Text == "4MByte (256 banks)") {
                    rom_size = 7;
                }
                else if (romsizebox.Text == "8MByte (512 banks)") {
                    rom_size = 8;
                }
                if (romsizebox.Text != "") { // Header text
                    Form1.headerTokens[2] = "ROM size: " + romsizebox.Text;
                    Program.gb_specify_rom_size(rom_size);
                }


                // Ram Size
                int ram_size = 0;
                if (ramsizebox.Text == "2 KBytes") {
                    ram_size = 1;
                }
                else if (ramsizebox.Text == "8 Kbytes") {
                    ram_size = 2;
                }
                else if (ramsizebox.Text == "32 KBytes (4 banks of 8KBytes each)") {
                    ram_size = 3;
                }
                else if (ramsizebox.Text == "128 KBytes (16 banks of 8KBytes each)") {
                    ram_size = 4;
                }
                else if (ramsizebox.Text == "64 KBytes (8 banks of 8KBytes each)") {
                    ram_size = 5;
                }
                else if (ramsizebox.Text == "512 bytes (nibbles)") {
                    ram_size = 6;
                }
                if (ram_size >= 1) {
                    Form1.headerTokens[3] = "RAM size: " + ramsizebox.Text;
                    Program.gb_specify_ram_size(ram_size);
                }
            }

            this.Close();
        }

        private void cartTypeBox_SelectedIndexChanged(object sender, EventArgs e) {
            if (cartTypeBox.Text == "32K Gameboy Flash Cart (Audio as WR)") {
                romsizebox.Text = "32KByte (no ROM banking)";
                ramsizebox.Text = "None";
            }
            else if (cartTypeBox.Text == "32K Gameboy Flash Cart (Normal WR)") {
                romsizebox.Text = "32KByte (no ROM banking)";
                ramsizebox.Text = "None";
            }
            else if (cartTypeBox.Text == "2M (BV5) Gameboy Flash Cart") {
                romsizebox.Text = "2MByte (128 banks)";
                ramsizebox.Text = "8 KBytes";
            }
            else if (cartTypeBox.Text == "1M (ES29LV160) Gameboy Flash Cart") {
                romsizebox.Text = "1MByte";
                ramsizebox.Text = "None";
            }
            else {
                romsizebox.Text = "";
                ramsizebox.Text = "";
            }
        }

        private void romsizebox_SelectedIndexChanged(object sender, EventArgs e) {

        }
    }
}