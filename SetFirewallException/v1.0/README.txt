SetFirewallException

Windows Firewall API�𗘗p���A�|�[�g�̒ǉ��E�폜���x���v���܂��B

�g�����F

SetFirewallException set    port tcp 80 "Web server"
SetFirewallException remove port tcp 80 ""

����1: "set" �� "remove"
����2: "port"
����3: "tcp" �� "udp"
����4: �|�[�g�ԍ�
����5: �A�v�����́B"remove"���g���ۂ͓s����A""���w�肵�܂��B

UAC���i�ɂ��܂��āF

requireAdministrator��ݒ�v���܂����B
