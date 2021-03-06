using System.Collections.Generic;
using System.Numerics;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;

namespace Trakx.Contracts.Set.WhiteList.ContractDefinition
{


    public partial class WhiteListDeployment : WhiteListDeploymentBase
    {
        public WhiteListDeployment() : base(BYTECODE) { }
        public WhiteListDeployment(string byteCode) : base(byteCode) { }
    }

    public class WhiteListDeploymentBase : ContractDeploymentMessage
    {
        public static string BYTECODE = "608060405234801561001057600080fd5b50604051610ed5380380610ed58339810180604052602081101561003357600080fd5b81019080805164010000000081111561004b57600080fd5b8201602081018481111561005e57600080fd5b815185602082028301116401000000008211171561007b57600080fd5b505060008054600160a060020a0319163317808255604051929550600160a060020a0316935091507f8be0079c531659141344cd1fd0a4f28419497f9722a3daafe3b4186f6b6457e0908290a360005b815181101561015b5760008282815181106100e257fe5b6020908102919091018101516003805460018082019092557fc2575a0e9e593c00f959f8c92f12db2869c3395a3b0502d05e2516446f71f85b018054600160a060020a031916600160a060020a0390931692831790556000918252600490925260409020805460ff1916821790559190910190506100cb565b5050610d698061016c6000396000f3fe608060405234801561001057600080fd5b50600436106100ec576000357c01000000000000000000000000000000000000000000000000000000009004806378446bc1116100a95780639303b16f116100835780639303b16f14610254578063e7d22fdb14610271578063edf26d9b146102c9578063f2fde38b146102e6576100ec565b806378446bc1146102205780638da5cb5b146102285780638f32d59b1461024c576100ec565b80631766486d146100f157806332ed010e14610120578063372c12b1146101a457806338eada1c146101ca5780634ba79dfe146101f2578063715018a614610218575b600080fd5b61010e6004803603602081101561010757600080fd5b503561030c565b60408051918252519081900360200190f35b6101906004803603602081101561013657600080fd5b81019060208101813564010000000081111561015157600080fd5b82018360208201111561016357600080fd5b8035906020019184602083028401116401000000008311171561018557600080fd5b50909250905061031e565b604080519115158252519081900360200190f35b610190600480360360208110156101ba57600080fd5b5035600160a060020a0316610382565b6101f0600480360360208110156101e057600080fd5b5035600160a060020a0316610397565b005b6101f06004803603602081101561020857600080fd5b5035600160a060020a03166106bf565b6101f0610804565b61010e61086c565b610230610872565b60408051600160a060020a039092168252519081900360200190f35b610190610882565b6101f06004803603602081101561026a57600080fd5b5035610893565b6102796108ec565b60408051602080825283518183015283519192839290830191858101910280838360005b838110156102b557818101518382015260200161029d565b505050509050019250505060405180910390f35b610230600480360360208110156102df57600080fd5b503561094e565b6101f0600480360360208110156102fc57600080fd5b5035600160a060020a0316610975565b60026020526000908152604090205481565b6000805b82811015610376576004600085858481811061033a57fe5b60209081029290920135600160a060020a03168352508101919091526040016000205460ff1661036e57600091505061037c565b600101610322565b50600190505b92915050565b60046020526000908152604090205460ff1681565b61039f610882565b6103a857600080fd5b6001546104b557600160a060020a03811660009081526004602052604090205460ff161561040a5760405160e560020a62461bcd02815260040180806020018281038252603b815260200180610d03603b913960400191505060405180910390fd5b6003805460018082019092557fc2575a0e9e593c00f959f8c92f12db2869c3395a3b0502d05e2516446f71f85b01805473ffffffffffffffffffffffffffffffffffffffff1916600160a060020a038416908117909155600081815260046020908152604091829020805460ff19169094179093558051918252517fa226db3f664042183ee0281230bba26cbf7b5057e50aee7f25a175ff45ce4d7f929181900390910190a16106bc565b60008036604051602001808383808284376040805191909301818103601f190182528352805160209182012060008181526002909252929020549195509093505050811515905061055457600082815260026020908152604091829020429081905582518581529182015281517f0e0905d1a972d476e353bdcc3e06b19a71709054c8ba01eccb7e0691eca6d374929181900390910190a150506106bc565b60015461056890829063ffffffff61098f16565b4210156105a95760405160e560020a62461bcd028152600401808060200182810382526034815260200180610c966034913960400191505060405180910390fd5b6000828152600260209081526040808320839055600160a060020a0386168352600490915290205460ff16156106135760405160e560020a62461bcd02815260040180806020018281038252603b815260200180610d03603b913960400191505060405180910390fd5b6003805460018082019092557fc2575a0e9e593c00f959f8c92f12db2869c3395a3b0502d05e2516446f71f85b01805473ffffffffffffffffffffffffffffffffffffffff1916600160a060020a038616908117909155600081815260046020908152604091829020805460ff19169094179093558051918252517fa226db3f664042183ee0281230bba26cbf7b5057e50aee7f25a175ff45ce4d7f929181900390910190a150505b50565b6106c7610882565b6106d057600080fd5b600160a060020a03811660009081526004602052604090205460ff1661072a5760405160e560020a62461bcd02815260040180806020018281038252603c815260200180610c5a603c913960400191505060405180910390fd5b61079781600380548060200260200160405190810160405280929190818152602001828054801561078457602002820191906000526020600020905b8154600160a060020a03168152600190910190602001808311610766575b50505050506109a890919063ffffffff16565b80516107ab91600391602090910190610bb6565b50600160a060020a038116600081815260046020908152604091829020805460ff19169055815192835290517f24a12366c02e13fe4a9e03d86a8952e85bb74a456c16e4a18b6d8295700b74bb9281900390910190a150565b61080c610882565b61081557600080fd5b60008054604051600160a060020a03909116907f8be0079c531659141344cd1fd0a4f28419497f9722a3daafe3b4186f6b6457e0908390a36000805473ffffffffffffffffffffffffffffffffffffffff19169055565b60015481565b600054600160a060020a03165b90565b600054600160a060020a0316331490565b61089b610882565b6108a457600080fd5b60015481116108e75760405160e560020a62461bcd028152600401808060200182810382526039815260200180610cca6039913960400191505060405180910390fd5b600155565b6060600380548060200260200160405190810160405280929190818152602001828054801561094457602002820191906000526020600020905b8154600160a060020a03168152600190910190602001808311610926575b5050505050905090565b6003818154811061095b57fe5b600091825260209091200154600160a060020a0316905081565b61097d610882565b61098657600080fd5b6106bc816109dd565b6000828201838110156109a157600080fd5b9392505050565b60606000806109b78585610a58565b91509150806109c557600080fd5b60606109d18684610abc565b50935061037c92505050565b600160a060020a0381166109f057600080fd5b60008054604051600160a060020a03808516939216917f8be0079c531659141344cd1fd0a4f28419497f9722a3daafe3b4186f6b6457e091a36000805473ffffffffffffffffffffffffffffffffffffffff1916600160a060020a0392909216919091179055565b81516000908190815b81811015610aab5784600160a060020a0316868281518110610a7f57fe5b6020026020010151600160a060020a03161415610aa357925060019150610ab59050565b600101610a61565b5060009250829150505b9250929050565b606060008084519050606060018203604051908082528060200260200182016040528015610af4578160200160208202803883390190505b50905060005b85811015610b4257868181518110610b0e57fe5b6020026020010151828281518110610b2257fe5b600160a060020a0390921660209283029190910190910152600101610afa565b50600185015b82811015610b9357868181518110610b5c57fe5b6020026020010151826001830381518110610b7357fe5b600160a060020a0390921660209283029190910190910152600101610b48565b5080868681518110610ba157fe5b60200260200101519350935050509250929050565b828054828255906000526020600020908101928215610c18579160200282015b82811115610c18578251825473ffffffffffffffffffffffffffffffffffffffff1916600160a060020a03909116178255602090920191600190910190610bd6565b50610c24929150610c28565b5090565b61087f91905b80821115610c2457805473ffffffffffffffffffffffffffffffffffffffff19168155600101610c2e56fe57686974654c6973742e72656d6f7665416464726573733a2041646472657373206973206e6f742063757272656e742077686974656c69737465642e54696d654c6f636b557067726164653a2054696d65206c6f636b20706572696f64206d757374206861766520656c61707365642e54696d654c6f636b557067726164653a204e657720706572696f64206d7573742062652067726561746572207468616e206578697374696e6757686974654c6973742e616464416464726573733a20416464726573732068617320616c7265616479206265656e2077686974656c69737465642ea165627a7a723058200569a259f6b629bdb4e28444575c2d5f0ccf19052d6a845e06ef264f722e6dc90029000000000000000000000000000000000000000000000000000000000000002000000000000000000000000000000000000000000000000000000000000000030000000000000000000000002260fac5e5542a773aa44fbcfedf7c193bc2c599000000000000000000000000c02aaa39b223fe8d0a0e5c4f27ead9083c756cc200000000000000000000000089d24a6b4ccb1b6faa2625fe562bdd9a23260359";
        public WhiteListDeploymentBase() : base(BYTECODE) { }
        public WhiteListDeploymentBase(string byteCode) : base(byteCode) { }
        [Parameter("address[]", "_initialAddresses", 1)]
        public virtual List<string> InitialAddresses { get; set; }
    }

    public partial class TimeLockedUpgradesFunction : TimeLockedUpgradesFunctionBase { }

    [Function("timeLockedUpgrades", "uint256")]
    public class TimeLockedUpgradesFunctionBase : FunctionMessage
    {
        [Parameter("bytes32", "", 1)]
        public virtual byte[] ReturnValue1 { get; set; }
    }

    public partial class AreValidAddressesFunction : AreValidAddressesFunctionBase { }

    [Function("areValidAddresses", "bool")]
    public class AreValidAddressesFunctionBase : FunctionMessage
    {
        [Parameter("address[]", "_addresses", 1)]
        public virtual List<string> Addresses { get; set; }
    }

    public partial class WhiteListFunction : WhiteListFunctionBase { }

    [Function("whiteList", "bool")]
    public class WhiteListFunctionBase : FunctionMessage
    {
        [Parameter("address", "", 1)]
        public virtual string ReturnValue1 { get; set; }
    }

    public partial class AddAddressFunction : AddAddressFunctionBase { }

    [Function("addAddress")]
    public class AddAddressFunctionBase : FunctionMessage
    {
        [Parameter("address", "_address", 1)]
        public virtual string Address { get; set; }
    }

    public partial class RemoveAddressFunction : RemoveAddressFunctionBase { }

    [Function("removeAddress")]
    public class RemoveAddressFunctionBase : FunctionMessage
    {
        [Parameter("address", "_address", 1)]
        public virtual string Address { get; set; }
    }

    public partial class RenounceOwnershipFunction : RenounceOwnershipFunctionBase { }

    [Function("renounceOwnership")]
    public class RenounceOwnershipFunctionBase : FunctionMessage
    {

    }

    public partial class TimeLockPeriodFunction : TimeLockPeriodFunctionBase { }

    [Function("timeLockPeriod", "uint256")]
    public class TimeLockPeriodFunctionBase : FunctionMessage
    {

    }

    public partial class OwnerFunction : OwnerFunctionBase { }

    [Function("owner", "address")]
    public class OwnerFunctionBase : FunctionMessage
    {

    }

    public partial class IsOwnerFunction : IsOwnerFunctionBase { }

    [Function("isOwner", "bool")]
    public class IsOwnerFunctionBase : FunctionMessage
    {

    }

    public partial class SetTimeLockPeriodFunction : SetTimeLockPeriodFunctionBase { }

    [Function("setTimeLockPeriod")]
    public class SetTimeLockPeriodFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "_timeLockPeriod", 1)]
        public virtual BigInteger TimeLockPeriod { get; set; }
    }

    public partial class ValidAddressesFunction : ValidAddressesFunctionBase { }

    [Function("validAddresses", "address[]")]
    public class ValidAddressesFunctionBase : FunctionMessage
    {

    }

    public partial class AddressesFunction : AddressesFunctionBase { }

    [Function("addresses", "address")]
    public class AddressesFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }

    public partial class TransferOwnershipFunction : TransferOwnershipFunctionBase { }

    [Function("transferOwnership")]
    public class TransferOwnershipFunctionBase : FunctionMessage
    {
        [Parameter("address", "newOwner", 1)]
        public virtual string NewOwner { get; set; }
    }

    public partial class AddressAddedEventDTO : AddressAddedEventDTOBase { }

    [Event("AddressAdded")]
    public class AddressAddedEventDTOBase : IEventDTO
    {
        [Parameter("address", "_address", 1, false )]
        public virtual string Address { get; set; }
    }

    public partial class AddressRemovedEventDTO : AddressRemovedEventDTOBase { }

    [Event("AddressRemoved")]
    public class AddressRemovedEventDTOBase : IEventDTO
    {
        [Parameter("address", "_address", 1, false )]
        public virtual string Address { get; set; }
    }

    public partial class UpgradeRegisteredEventDTO : UpgradeRegisteredEventDTOBase { }

    [Event("UpgradeRegistered")]
    public class UpgradeRegisteredEventDTOBase : IEventDTO
    {
        [Parameter("bytes32", "_upgradeHash", 1, false )]
        public virtual byte[] UpgradeHash { get; set; }
        [Parameter("uint256", "_timestamp", 2, false )]
        public virtual BigInteger Timestamp { get; set; }
    }

    public partial class OwnershipTransferredEventDTO : OwnershipTransferredEventDTOBase { }

    [Event("OwnershipTransferred")]
    public class OwnershipTransferredEventDTOBase : IEventDTO
    {
        [Parameter("address", "previousOwner", 1, true )]
        public virtual string PreviousOwner { get; set; }
        [Parameter("address", "newOwner", 2, true )]
        public virtual string NewOwner { get; set; }
    }

    public partial class TimeLockedUpgradesOutputDTO : TimeLockedUpgradesOutputDTOBase { }

    [FunctionOutput]
    public class TimeLockedUpgradesOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }

    public partial class AreValidAddressesOutputDTO : AreValidAddressesOutputDTOBase { }

    [FunctionOutput]
    public class AreValidAddressesOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("bool", "", 1)]
        public virtual bool ReturnValue1 { get; set; }
    }

    public partial class WhiteListOutputDTO : WhiteListOutputDTOBase { }

    [FunctionOutput]
    public class WhiteListOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("bool", "", 1)]
        public virtual bool ReturnValue1 { get; set; }
    }







    public partial class TimeLockPeriodOutputDTO : TimeLockPeriodOutputDTOBase { }

    [FunctionOutput]
    public class TimeLockPeriodOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }

    public partial class OwnerOutputDTO : OwnerOutputDTOBase { }

    [FunctionOutput]
    public class OwnerOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("address", "", 1)]
        public virtual string ReturnValue1 { get; set; }
    }

    public partial class IsOwnerOutputDTO : IsOwnerOutputDTOBase { }

    [FunctionOutput]
    public class IsOwnerOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("bool", "", 1)]
        public virtual bool ReturnValue1 { get; set; }
    }



    public partial class ValidAddressesOutputDTO : ValidAddressesOutputDTOBase { }

    [FunctionOutput]
    public class ValidAddressesOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("address[]", "", 1)]
        public virtual List<string> ReturnValue1 { get; set; }
    }

    public partial class AddressesOutputDTO : AddressesOutputDTOBase { }

    [FunctionOutput]
    public class AddressesOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("address", "", 1)]
        public virtual string ReturnValue1 { get; set; }
    }


}
