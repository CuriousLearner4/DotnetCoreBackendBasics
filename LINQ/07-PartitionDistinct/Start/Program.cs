using LINQSamples;

// Create instance of view model
SamplesViewModel vm = new();

// Call Sample Method
var result = vm.TakeQuery();
vm.Display(result);
result = vm.TakeMethod();
vm.Display(result);
result = vm.TakeRangeQuery();
vm.Display(result);
result = vm.TakeRangeMethod();
vm.Display(result);
result = vm.TakeWhileQuery();
vm.Display(result);
result = vm.SkipQuery();
vm.Display(result);
result = vm.SkipMethod();
vm.Display(result);
result = vm.SkipWhileQuery();
vm.Display(result);
result = vm.SkipWhileMethod();
vm.Display(result);
vm.Display(vm.DistinctQuery());
vm.Display(vm.DistinctWhere());
vm.Display(vm.DistinctByMethod());
vm.Display(vm.DistinctByQuery());
vm.Display(vm.ChunkQuery());
vm.Display(vm.ChunkMethod());
